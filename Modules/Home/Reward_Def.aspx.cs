using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vllib;

public partial class Modules_Home_Reward_Def : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string short_Description(string desc)
    {
        string sdesc = "";

        sdesc = dbc.getLimitedWords(desc, 20);

        return sdesc;
    }

    protected string getColor(string isread)
    {
        if (isread.Equals("True"))
        {
            return "readcls";
        }
        else
        {
            return "unreadcls";
        }
    }
}