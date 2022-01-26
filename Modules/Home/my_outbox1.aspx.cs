using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using vllib;

public partial class Modules_my_outbox : System.Web.UI.Page
{
    SqlConnection con = dbc.con;
    SqlCommand cmd = new SqlCommand();
    SqlDataReader dr;

    string cate_str, cate_str1;

    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();

    PagedDataSource pgds = new PagedDataSource();
    PagedDataSource pgds1 = new PagedDataSource();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //switch (Session["vl_usertype"].ToString())
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
    protected void Page_Init(object sender, System.EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["vl_userid"].ToString()))
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        
    }


    protected void Page_LoadComplete(object sender, System.EventArgs e)
    {
        Msgs_Bind();
        TaskActivity_Bind();
    }

    public string b_msg(string msgid)
    {

        string st = "";

        con.Close();
        cmd = new SqlCommand("SELECT user_Auth_tbl.username, msgs_tbl.* FROM (msgs_tbl INNER JOIN user_Auth_tbl ON msgs_tbl.frndid = user_Auth_tbl.uid) WHERE (msgs_tbl.msgid = '" + msgid + "')", con);
        con.Open();

        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dr.Read())
        {
            st = dr["dispname"].ToString() + " - " + dr["smsg"].ToString();
        }

        con.Close();

        return st;
    }

    protected void DataList1_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        HiddenField hfmid = (HiddenField)e.Item.FindControl("hf_mid");


        try
        {
            con.Close();
            string instr = "update msgs_tbl set show2 = @show2 where msgid = @msgid";
            cmd = new SqlCommand(instr, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@show2", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@msgid", SqlDbType.VarChar).Value = hfmid.Value;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        catch
        {
        }

        DataList1.SelectedIndex = -1;

        cate_str = "SELECT msgs_tbl.msgid, msgs_tbl.frndid, msgs_tbl.smsg, msgs_tbl.msg, msgs_tbl.uid, msgs_tbl.posted_date, msgs_tbl.show1, msgs_tbl.show2, msgs_tbl.read_msg, user_Auth_tbl.username FROM (msgs_tbl INNER JOIN user_Auth_tbl ON msgs_tbl.frndid = user_Auth_tbl.uid) WHERE (msgs_tbl.show2 = @show2) AND (msgs_tbl.uid = @uid) ORDER BY msgs_tbl.posted_date DESC";
        cmd = new SqlCommand(cate_str, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add("@show2", SqlDbType.Bit).Value = true;
        cmd.Parameters.Add("@uid", SqlDbType.VarChar).Value = Session["vl_userid"].ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        pgds.DataSource = ds.Tables[0].DefaultView;
        pgds.AllowPaging = true;
        pgds.PageSize = 10;

        if (string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            pgds.CurrentPageIndex = 0;
        }
        else
        {
            pgds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
        }

        DataList1.DataSource = pgds;
        DataList1.DataBind();

    }
    private void Msgs_Bind()
    {
        cate_str = "SELECT msgs_tbl.msgid, msgs_tbl.frndid, msgs_tbl.smsg, msgs_tbl.msg, msgs_tbl.uid, msgs_tbl.posted_date, msgs_tbl.show1, msgs_tbl.show2, msgs_tbl.read_msg FROM msgs_tbl WHERE (msgs_tbl.show2 = @show2) AND (msgs_tbl.uid = @uid) ORDER BY msgs_tbl.posted_date DESC";
        SqlCommand cmd = new SqlCommand(cate_str, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add("@show2", SqlDbType.Bit).Value = true;
        cmd.Parameters.Add("@uid", SqlDbType.VarChar).Value = Session["vl_userid"].ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        pgds.DataSource = ds.Tables[0].DefaultView;
        pgds.AllowPaging = true;
        pgds.PageSize = 10;

        if (string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            pgds.CurrentPageIndex = 0;
        }
        else
        {
            pgds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
        }

        DataList1.DataSource = pgds;
        DataList1.DataBind();


        if (pgds.IsLastPage)
        {
            HyperLink3.Visible = false;
            HyperLink5.Visible = false;
        }
        else
        {
            HyperLink3.Visible = true;
            HyperLink3.NavigateUrl = "my_outbox.aspx?pg=" + (pgds.CurrentPageIndex + 1).ToString();
            HyperLink5.Visible = true;
            HyperLink5.NavigateUrl = "my_outbox.aspx?pg=" + (pgds.CurrentPageIndex + 1).ToString();
        }

        if (pgds.IsFirstPage)
        {
            HyperLink2.Visible = false;
            HyperLink4.Visible = false;
        }
        else
        {
            HyperLink2.Visible = true;
            HyperLink2.NavigateUrl = "my_outbox.aspx?pg=" + (pgds.CurrentPageIndex - 1).ToString();
            HyperLink4.Visible = true;
            HyperLink4.NavigateUrl = "my_outbox.aspx?pg=" + (pgds.CurrentPageIndex - 1).ToString();
        }

        if (DataList1.Items.Count == 0)
        {
            nomsgsPanel1.Visible = true;
        }
        else
        {
            nomsgsPanel1.Visible = false;
        }

    }
    private void TaskActivity_Bind()
    {
        cate_str1 = "SELECT * from yantra_hr_Circular WHERE (yantra_hr_Circular.Company_Id In (55,56,57)) AND (yantra_hr_Circular.Dept_ID = @uid) ORDER BY yantra_hr_Circular.CIR_DATE DESC";
        SqlCommand cmd1 = new SqlCommand(cate_str1, con);
        cmd1.CommandType = CommandType.Text;
        cmd1.Parameters.Add("@show2", SqlDbType.Bit).Value = true;
        cmd1.Parameters.Add("@uid", SqlDbType.VarChar).Value = Session["vl_Empid"].ToString();

        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

        da1.Fill(ds1);
        pgds1.DataSource = ds1.Tables[0].DefaultView;
        pgds1.AllowPaging = true;
        pgds1.PageSize = 10;

        if (string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            pgds1.CurrentPageIndex = 0;
        }
        else
        {
            pgds1.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
        }

        DataList2.DataSource = pgds1;
        DataList2.DataBind();


        if (pgds1.IsLastPage)
        {
            HyperLink3.Visible = false;
            HyperLink5.Visible = false;
        }
        else
        {
            HyperLink3.Visible = true;
            HyperLink3.NavigateUrl = "my_outbox.aspx?pg=" + (pgds1.CurrentPageIndex + 1).ToString();
            HyperLink5.Visible = true;
            HyperLink5.NavigateUrl = "my_outbox.aspx?pg=" + (pgds1.CurrentPageIndex + 1).ToString();
        }

        if (pgds1.IsFirstPage)
        {
            HyperLink2.Visible = false;
            HyperLink4.Visible = false;
        }
        else
        {
            HyperLink2.Visible = true;
            HyperLink2.NavigateUrl = "my_outbox.aspx?pg=" + (pgds1.CurrentPageIndex - 1).ToString();
            HyperLink4.Visible = true;
            HyperLink4.NavigateUrl = "my_outbox.aspx?pg=" + (pgds1.CurrentPageIndex - 1).ToString();
        }

        if (DataList2.Items.Count == 0)
        {
            notaskpnl.Visible = true;
        }
        else
        {
            notaskpnl.Visible = false;
        }
    }
    public string get_date(System.DateTime d1)
    {
        string st = null;
        //st = d1.Day.ToString() + "-" + DateAndTime.MonthName(d1.Month.ToString(), true) + "-" + d1.Year.ToString() + " " + string.Format("{0:t}", DateAndTime.TimeValue(d1.ToString()));

        //st = dbc.RelativeDate(d1);
        st = d1.ToString("D");

        return st;
    }
    public Modules_my_outbox()
    {
        LoadComplete += Page_LoadComplete;
        Load += Page_Load;
        Init += Page_Init;
    }

    protected string getUserName(string uid)
    {
        return usre.getUsername(uid);
    }
    protected string getUserName1(string uid)
    {
        return getUsername1(uid);
    }
    public string getUsername1(string uid)
    {
        SqlCommand sqlCommand = new SqlCommand();
        string outcome = "";
        try
        {
            con.Close();
            sqlCommand = new SqlCommand("Select USER_NAME from YANTRA_USER_DETAILS where emp_id = " + uid, con);
            con.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            bool bool_ = sqlDataReader.Read();
            if (bool_)
            {
                outcome = sqlDataReader[0].ToString();
            }
        }
        catch (Exception)
        {
        }
        finally
        {
            con.Close();
        }
        return outcome;
    }

    protected void lnkbtnTasklist_Click(object sender, EventArgs e)
    {
        pnlTasklist.Visible = true;
        DataList1.Visible = false;
        notaskpnl.Visible = true;
        //TaskActivity_Bind();
    }
    protected void lnkbtnmsgs_Click(object sender, EventArgs e)
    {
        pnlTasklist.Visible = false ;
        DataList1.Visible = true ;
        nomsgsPanel1.Visible = true;
        //Msgs_Bind();
    }
}

