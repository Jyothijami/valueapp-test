using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Modules_HR_EmployeeMailFormat : System.Web.UI.Page
{   
    protected void Page_Load(object sender, EventArgs e)
    {
      //  Database db = new Database("(local)", "Vlt2", "sa", "Datumsql");
      //AddControls(db);
      //db.Close();


        string st = @"Index: <span id='ctl02_ctl00_Label1'>##fullname##</span>
    <br />
    Message: <span id='ctl02_ctl00_Label2'></span>
    <hr />
  
    Index: <span id='ctl02_ctl01_Label1'></span>
    <br />
    Message: <span id='ctl02_ctl01_Label2'></span>
    <hr />
  
    Index: <span id='ctl02_ctl02_Label1'></span>
    <br />
    Message: <span id='ctl02_ctl02_Label2'></span>
    <hr />
  
    Index: <span id='ctl02_ctl03_Label1'>##fullname##</span>
    <br />
    Message: <span id='ctl02_ctl03_Label2'></span>
    <hr />";

        st.Replace("##fullname##", "");







    }
   // protected override void OnInit(EventArgs e)
   //{
   //   base.OnInit(e);
   //   rptFields.ItemDataBound +=
   //      new RepeaterItemEventHandler(rptFields_ItemDataBound);
   //}
 
  
   //private void AddControls(Database db)
   //{
   //   DataTable dt = db.GetDataTableAdhoc("SELECT * FROM Parameters
   //                                        ORDER BY pkParameterID");
   //   rptFields.DataSource = dt;
   //   rptFields.DataBind();
   //}
 
   //void rptFields_ItemDataBound(object sender, RepeaterItemEventArgs e)
   //{
   //   if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType
   //       != ListItemType.AlternatingItem)
   //      return;
 
   //   DataRow dr = ((DataRowView)e.Item.DataItem).Row;
   //   PlaceHolder pl = (PlaceHolder)e.Item.FindControl("plControl");
   //   switch (dr["DataType"].ToString().ToLower())
   //   {
   //      case "string":
   //         TextBox txt = new TextBox();
   //         txt.ID = "txtField" + dr["pkParameterID"].ToString();
   //         pl.Controls.Add(txt);
   //         break;
 
   //      case "tf":
   //         CheckBox chk = new CheckBox();
   //         chk.ID = "chkField" + dr["pkParameterID"].ToString();
   //         pl.Controls.Add(chk);
   //         break;
   //   }
   //}
}