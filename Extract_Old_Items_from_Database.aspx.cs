using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YantraBLL;
using YantraBLL.Modules;
using vllib;

public partial class waste7 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        start();
    }

    protected void start()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon2"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = default(SqlDataReader);

        con.Close();
        cmd = new SqlCommand("SELECT * FROM YANTRA_ITEM_MAST", con);
        con.Open();
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dr.Read())
        {
            string itemcode = dr["ITEM_CODE"].ToString();
            string imagename = "";
            string specname = "";

            try
            {
                imagename = "ITP_" + itemcode + ".jpg";

                string filepath = Server.MapPath("~/Content/temp/") + imagename;

                byte[] barrImg = (byte[])dr["IMAGE"];
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write);
                fs.Write(barrImg, 0, barrImg.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception) { }

            try
            {
                specname = "SPEC_" + itemcode + ".jpg";

                string filepath = Server.MapPath("~/Content/temp/") + specname;

                byte[] barrImg = (byte[])dr["SPECIFICATION_IMAGE"];
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write);
                fs.Write(barrImg, 0, barrImg.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception) { }

            Masters.ItemMaster im = new Masters.ItemMaster();
            im.ItemCode = dr["ITEM_CODE"].ToString();
            im.ItemName = dr["ITEM_NAME"].ToString();
            im.ItemSpec = dr["ITEM_SPEC"].ToString();
            im.ItemMaterialType = dr["ITEM_MATERIAL_TYPE"].ToString();
            im.ItemtypeId = dr["IT_TYPE_ID"].ToString();
            im.Uomid = dr["UOM_ID"].ToString();
            im.Principalname = dr["ITEM_PRINCIPAL_NAME"].ToString();
            im.Itemseries = dr["ITEM_SERIES"].ToString();
            im.Purchasespec = dr["ITEM_PURCHASE_SPEC"].ToString();
            im.ModelNo = dr["ITEM_MODEL_NO"].ToString();
            im.IcId = dr["IC_ID"].ToString();
            im.Brandid = dr["BRAND_ID"].ToString();
            im.Barcode = dr["ITEM_CODE"].ToString();
            im.Status = "0";

            if (im.ItemMaster_Save() == "Data Saved Successfully")
            {
                if (imagename != "")
                {
                    im.ItemImage = imagename;
                    im.ItemDate = sdate.getDateTime().ToString();
                    im.ItemImage_Save();
                }

                if (specname != "")
                {
                    im.ItemSpec = specname;
                    im.Specdate = sdate.getDateTime().ToString();
                    im.SpecImage_Save();
                }

            }

            Label1.Text = Label1.Text + " " + itemcode;
        }

        
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        start2();
    }

    protected void start2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr = default(SqlDataReader);

        con.Close();
        cmd = new SqlCommand("SELECT EMP_ID, EMP_PHOTO FROM YANTRA_EMPLOYEE_MAST", con);
        con.Open();
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dr.Read())
        {
            string EMP_ID = dr["EMP_ID"].ToString();
            string imagename = "";

            try
            {
                imagename = "EMP_" + EMP_ID + ".jpg";

                string filepath = Server.MapPath("~/Content/temp/") + imagename;

                byte[] barrImg = (byte[])dr["EMP_PHOTO"];
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                FileStream fs = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write);
                fs.Write(barrImg, 0, barrImg.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception) { }

            updateEMPPic(EMP_ID, imagename);

            //Label1.Text = Label1.Text + " " + itemcode;
        }


        con.Close();
    }

    private static bool updateEMPPic(string empid, string emppicname)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            con.Close();
            string instr = "update YANTRA_EMPLOYEE_MAST set EMP_PHOTOO = @EMP_PHOTOO where EMP_ID = @EMP_ID";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@EMP_PHOTOO", SqlDbType.VarChar).Value = emppicname;
            cmd.Parameters.Add("@EMP_ID", SqlDbType.BigInt).Value = empid;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return true;
        }
        catch (Exception)
        { }
        finally
        {
            con.Close();
        }
        return false;
    }


}