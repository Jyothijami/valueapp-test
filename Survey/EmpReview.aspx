<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpReview.aspx.cs" Inherits="Survey_EmpReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
   
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
     <meta charset="utf-8" />
    <title>Feedback</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="author" content="colorlib.com" />

    <!-- MATERIAL DESIGN ICONIC FONT -->
    <link rel="stylesheet" href="fonts/material-design-iconic-font/css/material-design-iconic-font.css" />

    <!-- STYLE CSS -->
    <link rel="stylesheet" href="css/style.css" />
        <link rel="stylesheet" href="./index.css" />

    <style>
@import url(http://fonts.googleapis.com/css?family=Roboto:500,100,300,700,400);

* {
  margin: 0;
  padding: 0;
  font-family: roboto;
}

/*body { background: #000; }*/

.cont {
  width: 93%;
  max-width: 350px;
  text-align: center;
  margin: 4% auto;
  padding: 20px 0;
  background: #111;
  color: #EEE;
  border-radius: 5px;
  border: thin solid #444;
  overflow: hidden;
}
.container{
    
   display:inline-block;
  position: relative;
  padding-left: 10px;
  margin-bottom: 10px;
  cursor: pointer;
  font-size: 100%;
     margin-left: 5px;
  margin-top: 2px;
  border-radius: 10px;
  margin:-2px;
  line-height :30px;
 
  
}

hr {
  margin: 20px;
  border: none;
  border-bottom: thin solid rgba(255,255,255,.1);
}

div.title { font-size: 2em; }

h1 span {
  font-weight: 300;
  color: #Fd4;
}

div.stars {
  width: 370px;
  display: inline-block;
}

input.star { display: none; }

label.star {
  float: right;
  padding: 15px;
  font-size: 46px;
  color: #444;
  transition: all .2s;
}

input.star:checked ~ label.star:before {
  content: '\f005';
  color: #FD4;
  transition: all .25s;
}

input.star-5:checked ~ label.star:before {
  color: #6d7f52;
  text-shadow: 0 0 20px #952;
}
input.star-4:checked ~ label.star:before {
  color: lightgreen;
  text-shadow: 0 0 20px #952;
}
input.star-2:checked ~ label.star:before {
  color: #F62;
  /*text-shadow: 0 0 20px #952;*/
}

input.star-1:checked ~ label.star:before { color: Red; }

label.star:hover { transform: rotate(-15deg) scale(1.3); }

label.star:before {
  content: '\f006';
  font-family: FontAwesome;
}
</style>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.12.0/jquery.validate.min.js"  
type="text/javascript"></script>  
   <link href="http://www.cssscript.com/wp-includes/css/sticky.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" />
        <link href="https://surveyjs.azureedge.net/1.0.69/survey.css" type="text/css" rel="stylesheet" />

     <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
     <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
    <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
        media="screen" />
  
    

     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "Images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "Images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager runat ="server"  > </asp:ScriptManager>
    <div>
    <asp:GridView ID="gvEmployeeMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true" OnRowDataBound="gvEmployeeMaster_RowDataBound"
                            DataKeyNames="EMP_ID"  meta:resourcekey="gvEmployeeMasterResource1" DataSourceID ="sdsEmployeeMaster" Width="100%" >
                            <Columns>
                                <asp:BoundField HeaderText="Sl.No" Visible ="false"  SortExpression="Sl.No" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                                <asp:BoundField DataField="EMP_ID" SortExpression="" Visible ="false"  HeaderText="Emp Id">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField ReadOnly="True" DataField="EMP_ID" SortExpression="EMP_ID" Visible ="false"  HeaderText="EMPIDHidden" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
                               <asp:BoundField DataField ="Emp_Name" HeaderText ="Emp Name" />
                                
                                <asp:BoundField DataField="Emp_Dept" SortExpression="Emp_Dept" HeaderText="Department" meta:resourceKey="BoundFieldResource5">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMp_Desg" SortExpression="EMp_Desg" HeaderText="Designation" meta:resourceKey="BoundFieldResource6"></asp:BoundField>
                                <asp:BoundField DataField="Emp_DOJ" SortExpression="Emp_DOJ" HeaderText="DOJ" meta:resourceKey="BoundFieldResource7"></asp:BoundField>
                                <asp:BoundField DataField="Emp_Comp" SortExpression="Emp_Comp" HeaderText="Company" meta:resourceKey="BoundFieldResource9">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="locname" SortExpression="locname" HeaderText="locname"></asp:BoundField>
                                <%--<asp:BoundField HtmlEncode="False" SortExpression="EMP_DET_DOJ" DataFormatString="{0:dd/MM/yyyy}" DataField="EMP_DET_DOJ" HeaderText="DOJ"></asp:BoundField>--%>
                               
                                <%--<asp:BoundField DataField ="Status" HeaderText ="Status" />--%>

                                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Image ID="Image1" Style="height: 40px; cursor: pointer;" src="Images/plus.png" runat="server" />

                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">

                             <p>1. Ability to Accomplish Responsibilities</p>
							
							<div class="form-row">
								<div class="form-holder w-100">
                                    <div class="radiobtn">
                                            <asp:RadioButtonList ID="rdbRecommend" CssClass="checkbox-circle" EnableTheming ="false"  runat ="server"  >
                                               <%-- <asp:ListItem >Yes</asp:ListItem>
                                                <asp:ListItem >Maybe</asp:ListItem>
                                                <asp:ListItem >No</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                            <%--<span class ="checkmark"></span>--%>
                                        <div class="stars">
                                            <asp:TextBox CssClass="star star-5" runat="server"></asp:TextBox>
                                            <input class="star star-5" id="star-5-2" type="radio" name="star" value="5" />

                                            <label class="star star-5" for="star-5-2"></label>
                                            <input class="star star-4" id="star-4-2" type="radio" name="star" value="4" />
                                            <label class="star star-4" for="star-4-2"></label>
                                            <input class="star star-3" id="star-3-2" type="radio" name="star" value="3" />
                                            <label class="star star-3" for="star-3-2"></label>
                                            <input class="star star-2" id="star-2-2" type="radio" name="star" value="2" />
                                            <label class="star star-2" for="star-2-2"></label>
                                            <input class="star star-1" id="star-1-2" type="radio" name="star" value="1" />
                                            <label class="star star-1" for="star-1-2"></label>
                                        </div>
                                        </div> 
								</div>
                              
                               
							</div>
                           
                        

                            <%--<asp:GridView ID="gvdetails" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" HeaderStyle-CssClass="thead-inverse" Width="100%">
                                <Columns >
                                    <asp:TemplateField >
                                        <ItemTemplate >
                                            <asp:RadioButton ID="rdb" runat ="server" Text ="hi" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:BoundField DataField="" SortExpression="" HeaderText="locname"></asp:BoundField>
 
                                  
                                </Columns>
                                 <EmptyDataTemplate>
                    <span style="color: #CC0000">No Data to Display</span>
                           </EmptyDataTemplate>
                            </asp:GridView>--%>
                        </asp:Panel>
                    </ItemTemplate>
                    <ItemStyle Width="40px" />
                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="LightSteelBlue" />
                        </asp:GridView>
         <asp:SqlDataSource ID="sdsEmployeeMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="[USP_Emp_Master_Search]" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                               <%-- <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>
   --%>                             <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="ReportingTo" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>

    </div>


         <%--<div class="cont">
  <div class="stars">
    
      <input class="star star-5" id="star-5-2" type="radio" name="star"/>
      <label class="star star-5" for="star-5-2"></label>
      <input class="star star-4" id="star-4-2" type="radio" name="star"/>
      <label class="star star-4" for="star-4-2"></label>
      <input class="star star-3" id="star-3-2" type="radio" name="star"/>
      <label class="star star-3" for="star-3-2"></label>
      <input class="star star-2" id="star-2-2" type="radio" name="star"/>
      <label class="star star-2" for="star-2-2"></label>
      <input class="star star-1" id="star-1-2" type="radio" name="star"/>
      <label class="star star-1" for="star-1-2"></label>
   
  </div>
</div>--%>
      
    </form>
</body>
</html>
