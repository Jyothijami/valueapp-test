using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Home_view_message : basePage
{
SqlConnection con = dbc.con;
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //switch (Session["sc_usertype"].ToString())
        //{
        //    case "0":
        //        this.Page.MasterPageFile = "~/adminMP1.master";
        //        break;
        //    case "1":
        //        this.Page.MasterPageFile = "~/uMP1.master";
        //        break;
        //    case "2":
        //        this.Page.MasterPageFile = "~/executiveMP1.master";
        //        break;
        //}
    }
	public string get_date(System.DateTime d1)
    {
        string st = null;
        //st = d1.Day.ToString() + "-" + DateAndTime.MonthName(d1.Month.ToString(), true) + "-" + d1.Year.ToString() + " " + string.Format("{0:t}", DateAndTime.TimeValue(d1.ToString()));

        //st = dbc.RelativeDate(d1);
        st = d1.ToString("D");

        return st;
    }

	public bool mark_read(string msgid)
	{


		try {
			con.Close();
            string instr = "update msgs_tbl set read_msg = @read_msg where msgid = @msgid";
			cmd = new SqlCommand(instr, con);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@read_msg", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@msgid", SqlDbType.VarChar).Value = msgid;

			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();

			return true;
		} catch(Exception) {

            con.Close();
			return false;
		}

	}

	protected void Page_Init(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(Session["vl_userid"].ToString())) {
			Response.Redirect("login.aspx");
		}
	}


	protected void Page_Load(object sender, System.EventArgs e)
	{
	}

	protected void Page_LoadComplete(object sender, System.EventArgs e)
	{
		mark_read(Request.QueryString["msgid"]);
	}

	protected void Button1_Click(object sender, System.EventArgs e)
	{
		HiddenField hf = new HiddenField();
		hf = (HiddenField)DataList1.Items[0].FindControl("HiddenField1");

		Label msglb = new Label();
		msglb = (Label)DataList1.Items[0].FindControl("msgLabel");

		//Dim frmhyp As New HyperLink
		//frmhyp = DataList1.Items(0).FindControl("frmhyplnk1")

		string frmuser = usre.getUsername(hf.Value);

		Label postdate = new Label();
		postdate = (Label)DataList1.Items[0].FindControl("posted_dateLabel");

		string msg = "";
		msg = msg + msg_tb1.Text;
		msg = msg + "<br /><br /><hr size='1px' style='color: #CCCCCC' title='Send Message' /><br /><br />";
		msg = msg + "<b>" + frmuser + "</b> sent at " + postdate.Text + "<br /><br />Message : <br />";
		msg = msg + msglb.Text;

		if (!string.IsNullOrEmpty(sub_tb1.Text) & !string.IsNullOrEmpty(msg_tb1.Text)) {
            if (msgs.send_message(hf.Value, sub_tb1.Text, msg, Session["vl_userid"].ToString()))
            {
                Label1.Text = "Message Sent Successfully";
                sub_tb1.Text = "";
                msg_tb1.Text = "";
            }
            else
            {
                Label1.Text = "Message Sending Failed";
            }
		} else {
			Label1.Text = "Subject and Message cannot be empty";
		}

	}

	protected void Page_PreLoad(object sender, System.EventArgs e)
	{
        if (msgs.is_msg_folder_inbox(Request.QueryString["msgid"], Session["vl_userid"].ToString()))
        {
            send_mesg_panel1.Visible = true;
            back_folder_hypl51.Text = "Back to inbox";
            back_folder_hypl51.NavigateUrl = "my_inbox.aspx";

            string uid12 = msgs.get_msg_uid(Request.QueryString["msgid"]);
            if (string.IsNullOrEmpty(uid12))
            {
                replylkbt1.Visible = false;
                send_mesg_panel1.Visible = false;
            }
            else
            {
                replylkbt1.Visible = true;
            }

        }
        else
        {
            send_mesg_panel1.Visible = false;
            replylkbt1.Visible = false;
            back_folder_hypl51.Text = "Back to Outbox";
            back_folder_hypl51.NavigateUrl = "my_outbox.aspx";
        }
	}

	protected void LinkButton2_Click(object sender, System.EventArgs e)
	{
		bool is_inbox = false;
		is_inbox = msgs.is_msg_folder_inbox(Request.QueryString["msgid"], Session["vl_userid"].ToString());

		string instr = "";

        if (is_inbox)
        {
            instr = "update msgs_tbl set show1 = @show1 where msgid = @msgid";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@show1", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@msgid", SqlDbType.VarChar).Value = Request.QueryString["msgid"];
        }
        else
        {
            instr = "update msgs_tbl set show2 = @show2 where msgid = @msgid";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@show2", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@msgid", SqlDbType.VarChar).Value = Request.QueryString["msgid"];
        }


		try {
			con.Close();

			con.Open();
			cmd.ExecuteNonQuery();
            


		} catch {
		} finally {
			con.Close();
		}

		if (is_inbox) {
			Response.Redirect("my_inbox.aspx");
		} else {
			Response.Redirect("my_outbox.aspx");
		}
	}


    protected void replylkbt1_Click(object sender, System.EventArgs e)
	{
	}

	public string getulink(string uid)
	{
        //if (usre.isAdmin(uid)) {
        //    return "Admin";
        //} else if (string.IsNullOrEmpty(uid)) {
        //    return "Guest";
        //} else {
        //    string lk = usre.get_profile_link(uid);
        //    string uname = usre.get_dispname(uid);

        //    string st = "";
        //    if (lk == "#") {
        //        return uname;
        //    } else {
        //        return "<a href='" + lk + "'>" + uname + "</a>";
        //    }
        //}
        return usre.getUsername(uid);
	}
    public Modules_Home_view_message()
	{
		PreLoad += Page_PreLoad;
		LoadComplete += Page_LoadComplete;
		Load += Page_Load;
		Init += Page_Init;
	}

    protected string getUserName(string uid)
    {
        return usre.getUsername(uid);
    }

    protected void lkbtForward1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Modules/home/sendMessage.aspx?t=Forward&msgid=" + Request.QueryString["msgid"]);
    }

    protected void delmesglkbt1_Click(object sender, EventArgs e)
    {
        if (msgs.delete_message(Request.QueryString["msgid"]))
        {
            Response.Redirect("my_inbox.aspx");
        }
    }

}
 
