using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

using YantraBLL.Modules;
using Yantra.MessageBox;
public partial class Modules_ContraVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDate.Text = DateTime.UtcNow.ToShortDateString();
            Company_Fill();
        }
    }

    private void Company_Fill()
    {
        try
        {
            Masters.CompanyProfile.Company_Select(ddlCompany);
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
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());
        SqlCommand cmd = new SqlCommand("select CP_ADDRESS, CP_EMAIL from YANTRA_COMP_PROFILE where CP_ID=@CP_ID ORDER BY CP_FULL_NAME ", con);
        cmd.Parameters.AddWithValue("@CP_ID", ddlCompany.SelectedItem.Value);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        lblAddress.Text = dt.Rows[0][0].ToString();
        lblEmail.Text = dt.Rows[0][1].ToString();

        string value = lblAddress.Text;

        string str = "";
        string[] lines = Regex.Split(value, ",");
        for (int i = 0; i < lines.Length; i++)
        {
            str = str + lines[i] + "<br/>";
        }
        lblAddress.Text = str;
    }
}
 
