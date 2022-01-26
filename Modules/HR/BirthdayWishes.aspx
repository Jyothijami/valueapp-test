<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BirthdayWishes.aspx.cs" Inherits="Modules_HR_BirthdayWishes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="wellcome.css" rel="stylesheet" />

    <style type="text/css">
         marquee{
            direction:rtl;
            
        }
        #lblmsg{
           font-size:xx-large;
           overflow: hidden;
        }
        #lblmsg:hover{
          cursor:pointer;
        }
     #lblbday{
         font-size:xx-large;
         color:green;

     }
        .txtmsg {
            background-color: #ff4d4d;
            color: white;
        }
        .txtmsg1{
            background-color:green;
            color:white;
        }
    </style>
          <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

</head>
<body style="background-image:url(/Images/asvasv.jpg);background-repeat:no-repeat; width :100%">
    <form id="form1" runat="server" >
    <div class="container">
    <div class="row">
        <div class="col-xs-6 col-sm-6 col-lg-6 col-md-6 ">
            <div >
                 <asp:GridView ID="gvempbday" runat="server" GridLines="None" EnableTheming ="false"  AutoGenerateColumns="false" DataSourceID="sqlds1" Height="100px" Width="350px">
              <Columns>
                  <asp:TemplateField>
                      <ItemTemplate>
                          <marquee direction="left" behaviour="alternate" onmousedown="this.stop();"onmouseup="this.start();" >
                              <asp:Label class="txtmsg" style="font-size:medium; font-family:cursive ">Wishing You a HAPPY BIRTHDAY </asp:Label>
                          <%--<asp:Label class="txtmsg1" ID="txtname" style="font-size:xx-large; font-family :Georgia" runat="server">Mr. Narinder Anand</asp:Label>--%>

                          <asp:Label class="txtmsg1" ID="txtname" style="font-size:xx-large; font-family :Georgia" runat="server" Text='<%#Bind("EMP_FIRST_NAME") %>'></asp:Label>
                      </marquee>
                              </ItemTemplate>
                  </asp:TemplateField>
                  
              </Columns>
                    
                     <EditRowStyle Font-Bold="False" Font-Italic="True" />
                    
          </asp:GridView>
                <marquee direction="down"   behavior="alternate">
            <marquee direction="left">
                <asp:Image ID="Image5" align="center" runat="server" Height="35px" Width="35px" ImageUrl="~/Images/VvEI.gif" />
            </marquee>
            <marquee direction="right">
                <asp:Image ID="Image6" align="center" runat="server" Height="35px" Width="35px" ImageUrl="~/Images/anib.gif" />
            </marquee>
                </marquee>
            </div>
            
             <div  style="height:56px">
            <marquee direction="down"  height="200" behavior="alternate">
            <marquee  behaviour="alternate" >
                <asp:Image ID="imgty" runat="server" Visible ="false"   ImageUrl="~/Images/154063772842764933.png" />
            </marquee>
                </marquee>
        </div>
        <div >
            <marquee direction="down"   behavior="alternate">
            <marquee direction="left">
                <asp:Image ID="Image1" align="center" runat="server" Height="35px" Width="35px" ImageUrl="~/Images/smily1.png" />
            </marquee>
            <marquee direction="right">
                <asp:Image ID="Image2" align="center" runat="server" Height="35px" Width="35px" ImageUrl="~/Images/smily2.png" />
            </marquee>
                </marquee>
         
        </div>
             <div>
            <asp:SqlDataSource ID="sqlds1" runat="server" ConnectionString="<%$connectionstrings:DBCon %>" SelectCommand="select EMP_FIRST_NAME+' '+EMP_LAST_NAME as EMP_FIRST_NAME from YANTRA_EMPLOYEE_MAST where DATEPART(M,EMP_DOB)=DATEPART(m,getdate()) and DATEPART(d,EMP_DOB)=DATEPART(d,getdate())  and STATUS=1" SelectCommandType="Text">
                
            </asp:SqlDataSource>
    </div>
        </div>
        <div class="col-xs-6 col-sm-6 col-lg-6 col-md-6 ">
            <div >
                <asp:ImageButton style="margin-left:100px"  ID="imgbtnnotify"  runat="server" Height="25px" Width="30px" ImageUrl="~/Images/sacasc.png" OnClick="imgbtnnotify_Click" />
            <asp:Label  ID="lblnotify"   ForeColor="Red" runat="server"></asp:Label>
                <br />
           <asp:GridView ID="gvwishes" style="margin-left:85px" AllowPaging ="True" PageSize ="5"  Visible="False" 
               DataSourceID="dswishes" runat="server" AutoGenerateColumns="False" AllowSorting="True" EnableTheming="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="335px" >
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pagination" VerticalAlign="Middle" ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="dt_added"  HeaderText="Date" />
                    <asp:BoundField DataField="logdesc" HeaderText="Wishes" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
     <asp:SqlDataSource ID="dswishes" runat="server" ConnectionString="<%$connectionstrings:DBCon %>" SelectCommand="select * from log_details_tbl1 where logcateid =140 and DATEPART(M,dt_added )=DATEPART(m,getdate()) and DATEPART(d,dt_added )=DATEPART(d,getdate()) order by dt_added desc" SelectCommandType="Text"></asp:SqlDataSource>

            </div>
            <div >
                  <marquee direction="down"   behavior="alternate">
            <marquee direction="right">
                <asp:Image ID="Image3" align="center" runat="server" Height="50px" Width="50px" ImageUrl="~/Images/b1.png" />
            </marquee>
            <marquee direction="left">
                <asp:Image ID="Image4" align="center" runat="server" Height="50px" Width="50px" ImageUrl="~/Images/b2.png" />
            </marquee>
                </marquee>
            </div>
        </div>
        <div style="margin-top:95px; text-align :right ">
            <asp:TextBox ID="txtwishes" TextMode ="MultiLine"  runat="server" BorderStyle="Inset" BorderColor="#CC00CC"></asp:TextBox>
            <asp:Button ID="btnwishme"  Text="Wish Here"  runat="server" OnClick="btnwishme_Click" BorderColor="#CC00CC" BorderStyle="Groove"   /> 
            </div>
    </div>
    </div>
    </form>
</body>
</html>
