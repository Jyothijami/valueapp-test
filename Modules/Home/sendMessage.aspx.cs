using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class Modules_sendMessage : basePage
{
    static string msgid;
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request.QueryString["t"] != null)
        {
            if (Request.QueryString["t"] == "Forward")
            {
                msgs m = new msgs(Request.QueryString["msgid"]);

                tbxsubj1.Text = m.msg_Subject;
                tbxmessage1.Text = m.msg_Body;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.SetActiveView(View1);
            BindData();
            string touid = Request.QueryString["uid"];
            string fromuid = Session["vl_userid"].ToString();

            //tbxuname1.Text = usre.getUsername(touid);
            hffromuid1.Value = fromuid;
            //hftouid1.Value = touid;

            msgid = dbc.get_rnum("msgid", "msgs_tbl");
        }
    }
    private void BindData()
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString()))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "SELECT USER_NAME,USER_ID FROM YANTRA_USER_DETAILS where EXPIRY_DATE >='2019-12-31 00:00:00.000' ORDER BY [USER_NAME]";
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dt);

                // BIND THE SELECT DROP-DOWN LIST WITH A DATABASE TABLE.
                Books.DataSource = dt;
                Books.DataTextField = "USER_NAME";
                Books.DataValueField = "USER_ID";

                Books.DataBind();

                // FOR MULTIPLE SELECTION. SET THE VALUE AS FALSE, AND SEE WHAT HAPPENS.
                Books.Multiple = true;
            }
        }


        // SM.CustomerMaster.CustomerMaster_Select(Books);


    }
    protected void btnSendMessage1_Click(object sender, EventArgs e)
    {
        if (msgs.send_message(ddluname1.SelectedValue, tbxsubj1.Text, tbxmessage1.Text, hffromuid1.Value, msgid))
        {
            MultiView1.SetActiveView(View2);
        }
    }


    protected void btnAttach1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string msgattid = dbc.get_rnum("msgattid", "msgs_Attachments_tbl");

            string filename = FileUpload1.FileName;
            string savefilename = msgattid + "_" + filename;

            FileUpload1.SaveAs(Server.MapPath("~/Content/messagesAttachments/") + savefilename);

            if (msgs.Attachments.add(filename, savefilename, msgid, msgattid))
            {
                DataTable dt = msgs.Attachments.getAttachments(msgid);

                DLAttachments1.DataSource = dt;
                DLAttachments1.DataBind();
            }
        }
    }

    protected void lkbtAddAttachments1_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View3);

        DataTable dt = msgs.Attachments.getAttachments(msgid);

        DLAttachments2.DataSource = dt;
        DLAttachments2.DataBind();

        DLAttachments1.DataSource = dt;
        DLAttachments1.DataBind();
    }

    protected void btnAttachDone1_Click(object sender, EventArgs e)
    {
        MultiView1.SetActiveView(View1);

        DataTable dt = msgs.Attachments.getAttachments(msgid);

        DLAttachments2.DataSource = dt;
        DLAttachments2.DataBind();

        DLAttachments1.DataSource = dt;
        DLAttachments1.DataBind();
    }
}
 
