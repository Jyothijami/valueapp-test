using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using vllib;

public partial class Modules_Home_my_inbox : basePage
{
    SqlConnection con = dbc.con;
	SqlCommand cmd = new SqlCommand();
	SqlDataReader dr;

	string cate_str;

	DataSet ds = new DataSet();

	PagedDataSource pgds = new PagedDataSource();

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
	public string b_msg(string msgid)
	{
		string st = "";

		con.Close();
        cmd = new SqlCommand("SELECT user_Auth_tbl.username, msgs_tbl.* FROM (msgs_tbl INNER JOIN user_Auth_tbl ON msgs_tbl.uid = user_Auth_tbl.uid) WHERE (msgs_tbl.msgid = '" + msgid + "')", con);
		con.Open();

		dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		if (dr.Read()) {
			st = dr["username"].ToString() + " - " + dr["smsg"].ToString();
		}

		con.Close();

		return st;
	}

	protected void DataList1_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
	{
		SqlCommand cmd = new SqlCommand();
		HiddenField hfmid = (HiddenField)e.Item.FindControl("hf_mid");


		try {
			con.Close();
            string instr = "update msgs_tbl set show1 = @show1 where msgid = @msgid";
			cmd = new SqlCommand(instr, con);
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@show1", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@msgid", SqlDbType.VarChar).Value = hfmid.Value;

			con.Open();
			cmd.ExecuteNonQuery();
			con.Close();

		} catch {
		}

		DataList1.SelectedIndex = -1;

        cate_str = "SELECT msgs_tbl.msgid, msgs_tbl.frndid, msgs_tbl.smsg, msgs_tbl.msg, msgs_tbl.uid, msgs_tbl.posted_date, msgs_tbl.show1, msgs_tbl.show2, msgs_tbl.read_msg, user_Auth_tbl.username FROM (msgs_tbl INNER JOIN user_Auth_tbl ON msgs_tbl.uid = user_Auth_tbl.uid) WHERE (msgs_tbl.show1 = @show1) AND (msgs_tbl.frndid = @frndid) ORDER BY msgs_tbl.posted_date DESC";
        cmd = new SqlCommand(cate_str, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add("@show1", SqlDbType.Bit).Value = true;
        cmd.Parameters.Add("@frndid", SqlDbType.VarChar).Value = Session["vl_userid"].ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);

		da.Fill(ds);
		pgds.DataSource = ds.Tables[0].DefaultView;
		pgds.AllowPaging = true;
		pgds.PageSize = 10;

		if (string.IsNullOrEmpty(Request.QueryString["pg"])) {
			pgds.CurrentPageIndex = 0;
		} else {
			pgds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
		}

		DataList1.DataSource = pgds;
		DataList1.DataBind();

	}

	protected void DataList1_EditCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
	{
		DataList1.EditItemIndex = e.Item.ItemIndex;

        cate_str = "SELECT msgs_tbl.msgid, msgs_tbl.frndid, msgs_tbl.smsg, msgs_tbl.msg, msgs_tbl.uid, msgs_tbl.posted_date, msgs_tbl.show1, msgs_tbl.show2, msgs_tbl.read_msg, user_Auth_tbl.username FROM (msgs_tbl INNER JOIN user_Auth_tbl ON msgs_tbl.uid = user_Auth_tbl.uid) WHERE (msgs_tbl.show1 = @show1) AND (msgs_tbl.frndid = @frndid) ORDER BY msgs_tbl.posted_date DESC";
        SqlCommand cmd = new SqlCommand(cate_str, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add("@show1", SqlDbType.Bit).Value = true;
        cmd.Parameters.Add("@frndid", SqlDbType.VarChar).Value = Session["vl_userid"].ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);

		da.Fill(ds);
		pgds.DataSource = ds.Tables[0].DefaultView;
		pgds.AllowPaging = true;
		pgds.PageSize = 10;

		if (string.IsNullOrEmpty(Request.QueryString["pg"])) {
			pgds.CurrentPageIndex = 0;
		} else {
			pgds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
		}

		DataList1.DataSource = pgds;
		DataList1.DataBind();
	}

	protected void DataList1_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		DataList1.EditItemIndex = -1;

        cate_str = "SELECT msgs_tbl.msgid, msgs_tbl.frndid, msgs_tbl.smsg, msgs_tbl.msg, msgs_tbl.uid, msgs_tbl.posted_date, msgs_tbl.show1, msgs_tbl.show2, msgs_tbl.read_msg, user_Auth_tbl.username FROM (msgs_tbl INNER JOIN user_Auth_tbl ON msgs_tbl.uid = user_Auth_tbl.uid) WHERE (msgs_tbl.show1 = @show1) AND (msgs_tbl.frndid = @frndid) ORDER BY msgs_tbl.posted_date DESC";
        SqlCommand cmd = new SqlCommand(cate_str, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.Add("@show1", SqlDbType.Bit).Value = true;
        cmd.Parameters.Add("@frndid", SqlDbType.VarChar).Value = Session["vl_userid"].ToString();

        SqlDataAdapter da = new SqlDataAdapter(cmd);

		da.Fill(ds);
		pgds.DataSource = ds.Tables[0].DefaultView;
		pgds.AllowPaging = true;
		pgds.PageSize = 10;

		if (string.IsNullOrEmpty(Request.QueryString["pg"])) {
			pgds.CurrentPageIndex = 0;
		} else {
			pgds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
		}

		DataList1.DataSource = pgds;
		DataList1.DataBind();

		HiddenField hfmid = (HiddenField) DataList1.Items[DataList1.SelectedIndex].FindControl("hf_mid");
		mark_read(hfmid.Value);


	}

	protected void Page_Init(object sender, System.EventArgs e)
	{
		if (string.IsNullOrEmpty(Session["vl_userid"].ToString())) {
			Response.Redirect("login.aspx");
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (DataList1.Items.Count == 0) {
			nomsgsPanel1.Visible = true;
		} else {
			nomsgsPanel1.Visible = false;
		}
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;
        lblfrndid.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.EmpId );
        lblShow1.Text = "1";

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
		} catch (Exception) {

            con.Close();
			return false;

		}

	}

	public System.Drawing.Color color_msg(bool read_msg)
	{

		if (read_msg) {
			return System.Drawing.Color.White;
		} else {
			return System.Drawing.Color.LightGray;
		}
	}
    #region DropDownList Search By Select Index Changed
    protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchBy.SelectedItem.Text == "Date")
        {
            ddlSymbols.Visible = true;
            txtSearchText.Visible = false;
            txtSearchValueFromDate.Visible = true;
            //imgToDate.Visible = true;
            //ceSearchValueToDate.Enabled = true;
            //meeSearchToDate.Enabled = true;
        }
        else
        {
            ddlSymbols.Visible = false;
            //imgToDate.Visible = false;
            //ceSearchValueToDate.Enabled = false;
            //meeSearchToDate.Enabled = false;
            txtSearchText.Visible = true;

            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //meeSearchFromDate.Enabled = false;
            ddlSymbols.SelectedIndex = 0;
        }
        txtSearchText.Text = string.Empty;
        txtSearchValueFromDate.Text = string.Empty;
        txtSearchValueToDate.Text = string.Empty;
    }
    #endregion

    #region DropDownList Symbols Select Index Changed
    protected void ddlSymbols_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSymbols.SelectedItem.Text == "R")
        {
            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = true;
            txtSearchText.Visible = false;
            lblCurrentFromDate.Visible = true;
            lblCurrentToDate.Visible = true;
            //imgFromDate.Visible = true;
            //ceSearchFrom.Enabled = true;
            //MaskedEditSearchFromDate.Enabled = true;
        }
        else
        {

            txtSearchValueFromDate.Visible = true;
            txtSearchValueToDate.Visible = false;
            lblCurrentFromDate.Visible = false;
            lblCurrentToDate.Visible = false;
            //imgFromDate.Visible = false;
            //ceSearchFrom.Enabled = false;
            //MaskedEditSearchFromDate.Enabled = false;
        }
    }
    #endregion

    protected void btnSearchGo_Click(object sender, EventArgs e)
    {
        DataList1.SelectedIndex = -1;
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchTypeHidden.Text = ddlSymbols.SelectedValue;
        if (ddlSearchBy.SelectedItem.Text == "Date")
        {
            if (ddlSymbols.SelectedItem.Text == "R")
            {
                txtSearchValueFromDate.Visible = true;
                lblSearchValueFromHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
                txtSearchValueToDate.Visible = true;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueToDate.Text);

            }
            else
            {
                txtSearchText.Visible = false;
                txtSearchValueFromDate.Visible = true;
                txtSearchValueToDate.Visible = false;
                lblSearchValueHidden.Text = Yantra.Classes.General.toMMDDYYYY(txtSearchValueFromDate.Text);
            }
        }
        else
        {
            txtSearchValueFromDate.Visible = false;
            txtSearchValueToDate.Visible = false;
            lblSearchValueHidden.Text = txtSearchText.Text;
        }
        //Page_PreLoad(sender,e);
        BindGrid();
    }
    protected void BindGrid()
    {
        lblSearchItemHidden.Text = ddlSearchBy.SelectedValue;
        lblSearchValueHidden.Text = txtSearchText.Text;

        lblfrndid.Text = Yantra.Authentication.GetEmployeeInSession(Yantra.Authentication.Logged_EMP_Details.UserId );
        lblShow1.Text = "1";
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand("SP_MSG_Search",con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@SearchItemName", lblSearchItemHidden.Text);
        cmd.Parameters.AddWithValue("@SearchValue", lblSearchValueHidden.Text);

        cmd.Parameters.AddWithValue("@SearchType", lblSearchTypeHidden.Text);
        cmd.Parameters.AddWithValue("@SearchValueFrom", lblSearchValueFromHidden.Text);

        cmd.Parameters.AddWithValue("@frndid", lblfrndid.Text);
        cmd.Parameters.AddWithValue("@show1", lblShow1.Text);
        //con.Open();
        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //PagedDataSource pds = new PagedDataSource();
        da1.Fill(dt);
        pgds.DataSource = dt.DefaultView;
        DataList1.DataSource = pgds;
        //pgds.DataSource = ds.Tables[0].DefaultView;
        pgds.AllowPaging = true;
        pgds.PageSize = 10;
        DataList1.DataBind();
    }
	protected void Page_PreLoad(object sender, System.EventArgs e)
	{
        ////cate_str = "SELECT msgs_tbl.msgid, msgs_tbl.frndid, msgs_tbl.smsg, msgs_tbl.msg, msgs_tbl.uid, msgs_tbl.posted_date, msgs_tbl.show1, msgs_tbl.show2, msgs_tbl.read_msg FROM msgs_tbl WHERE (msgs_tbl.show1 = @show1) AND (msgs_tbl.frndid = @frndid) ORDER BY msgs_tbl.posted_date DESC";
        ////SqlCommand cmd = new SqlCommand(cate_str, con);
        ////cmd.CommandType = CommandType.Text;
        ////cmd.Parameters.Add("@show1", SqlDbType.Bit).Value = true;
        ////cmd.Parameters.Add("@frndid", SqlDbType.VarChar).Value = Session["vl_userid"].ToString();

        ////SqlDataAdapter da = new SqlDataAdapter(cmd);

        ////da.Fill(ds);
        //pgds.DataSource = ds.Tables[0].DefaultView;
        //pgds.AllowPaging = true;
        //pgds.PageSize = 10;
        BindGrid();
		if (string.IsNullOrEmpty(Request.QueryString["pg"])) {
			pgds.CurrentPageIndex = 0;
		} else {
			pgds.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pg"]);
		}

		DataList1.DataSource = pgds;
		DataList1.DataBind();

		if (pgds.IsLastPage) {
			HyperLink3.Visible = false;
			HyperLink5.Visible = false;
		} else {
			HyperLink3.Visible = true;
			HyperLink3.NavigateUrl = "my_inbox.aspx?pg=" + (pgds.CurrentPageIndex + 1).ToString();
			HyperLink5.Visible = true;
			HyperLink5.NavigateUrl = "my_inbox.aspx?pg=" + (pgds.CurrentPageIndex + 1).ToString();
		}

		if (pgds.IsFirstPage) {
			HyperLink2.Visible = false;
			HyperLink4.Visible = false;
		} else {
			HyperLink2.Visible = true;
			HyperLink2.NavigateUrl = "my_inbox.aspx?pg=" + (pgds.CurrentPageIndex - 1).ToString();
			HyperLink4.Visible = true;
			HyperLink4.NavigateUrl = "my_inbox.aspx?pg=" + (pgds.CurrentPageIndex - 1).ToString();
		}
	}
    public Modules_Home_my_inbox()
	{
		PreLoad += Page_PreLoad;
		Load += Page_Load;
		Init += Page_Init;
	}

    protected string getUserName(string uid)
    {
        return usre.getUsername(uid);
    }
}
 
