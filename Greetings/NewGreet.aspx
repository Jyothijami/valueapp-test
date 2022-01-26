<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewGreet.aspx.cs" Inherits="NewGreet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Valueline</title>


   

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.0/jquery.min.js" type="text/javascript"></script>  
    <style>
        * {
  margin: 0 auto;
  /*padding: 0;*/
}

*:focus {
  outline: none;
}

body {
  margin-top: 70px;
  background-color: #f4f4f4;
  font-family: 'Courier New','Raleway', sans-serif;
  font-size: 62.5%;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

h1,
p,
a,
span {
  color: #000;
  letter-spacing: 0.02em;
  font-weight: 600;
}

h1,
p,
a {
  padding-left: 40px;
}

h1 {
  font-size: 2.5em;
}

p {
  font-size: 1.5em;
  line-height: 25px;
}

button {
  border: 0;
  border-radius: 26px;
  padding-bottom: 2px;
}

span {
  font-size: 1.6em;
}

.container,
.flip-box,
.front,
.back {
  /*width: 380px;
  height: 350px;*/

   width: 750px;
  height: 450px;
    margin-top: 10%;
}

.container,
.flip_box {
  position: relative;
}

.front,
.back {
  position: absolute;
  -webkit-backface-visibility: hidden;
  -moz-backface-visibility: hidden;
  backface-visibility: hidden;
}

.container {
  -moz-transform: perspective(1200px);
  -webkit-perspective: 1200px;
  perspective: 1200px;
}

.flip_box {
  transition: all 0.5s ease-out;
  -webkit-transform-style: preserve-3d;
  -moz-transform-style: preserve-3d;
  transform-style: preserve-3d;
}

.front {
  background-color: #024395;
  background-image: url('hb.jpg');
}

.f_title {
  padding-top: 30px;
}

.f_subline {
  padding-top: 205px;
  color: #B1D4E8;
}

.f_headline {
  line-height: 23px;
}

.back {
 background-color: #FFF;
  -webkit-transform: rotateY(180deg);
  -moz-transform: rotateY(180deg);
  -ms-transform: rotateY(180deg);
  transform: rotateY(180deg);
   
}

.b_headline {
  padding-top: 5px;
}

.b_text {
  font-size: 1.6em;
  line-height: 28px;
  padding-top: 8px;
  opacity: 0.85;
}

.b_button {
  position: absolute;
  left: 36px;
  bottom: 60px;
  width: 150px;
  /*height: 52px;
  background-color: #C30C3D;
  transition: all 0.3s;*/
}

.b_button:hover {
  
}

.flipped {
  -webkit-transform: rotateY(-180deg);
  -moz-transform: rotateY(-180deg);
  -ms-transform: rotateY(-180deg);
  transform: rotateY(-180deg);
}

.r_wrap {
  position: absolute;
  right: 20px;
  bottom: -65px;
}

.b_round,
.s_round {
  position: absolute;
  right: 0px;
  bottom: 0px;
  width: 52px;
  height: 52px;
  border-radius: 50%;
  background-color: #131313;
  transition: all 0.2s linear;
}

.b_round {
  opacity: 0;
  background-color: #0D0C0C;
}

.b_round_hover {
  transform: scale(1.37);
  opacity: 0.4;
}

.b_round_back_hover {
  background-color: #F60044;
}

.s_round_click {
  transform: scale(1.7);
}

.s_round_back {
  background-color: #131313;
}

.s_arrow {
  width: 52px;
  height: 52px;
  background-image: url('https://img-fotki.yandex.ru/get/194549/29644339.5/0_d6c60_1d7815f0_orig');
  background-color: transparent;
  transition: all 0.35s linear;
}

.s_arrow_rotate {
  transform: rotate(-180deg);





 



}
    </style>


    <style>
          #myVideo{
            position: fixed;
            right: 0;
            bottom: 0;
            min-width: 100%;
            min-height: 100%;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            var s_round = '.s_round';

            $(s_round).hover(function () {
                $('.b_round').toggleClass('b_round_hover');
                return false;
            });

            $(s_round).click(function () {
                $('.flip_box').toggleClass('flipped');
                $(this).addClass('s_round_click');
                $('.s_arrow').toggleClass('s_arrow_rotate');
                $('.b_round').toggleClass('b_round_back_hover');
                return false;
            });

            $(s_round).on('transitionend', function () {
                $(this).removeClass('s_round_click');
                $(this).addClass('s_round_back');
                return false;
            });
        });


    </script>













</head>
<body>
    <form id="form1" runat="server">
    <div>


         <video autoplay muted loop id="myVideo">
        <source src="Gbdr6INFdwYbaqxW.mp4" type="video/mp4"/>
    </video>



    <div class='container'>

  <div class='flip_box'>

    <div class='front'>

         <p class="b_text" style="text-align:center"><asp:Image ID="img1" Width="250px" runat ="server" ImageUrl ="~/Greetings/hpybdy.png" /></p>
     

      <asp:GridView ID="gvempbday" runat="server" GridLines="None" EnableTheming ="false"  AutoGenerateColumns="false" DataSourceID="sqlds1">
              <Columns>
                  <asp:TemplateField>
                      <ItemTemplate>
                          <div class="span2">
                               <h1 class='b_headline'>Dear <asp:Label ID="lblusername" Text='<%#Bind("EMP_FIRST_NAME") %>' runat="server" Font-Size="1.1em"></asp:Label>,</h1>

                           <%-- <div class="main-logo" >
                                <a href="#">
                                    <asp:Image ID="cpImage1" runat="server" ImageUrl='<%# Eval("EMP_PHOTO","~/Content/EmployeeImage/{0}") %>' Width="180px" Height="60px" /></a> 

                            </div>--%>
                        </div>
                              </ItemTemplate>
                  </asp:TemplateField>
                  
              </Columns>
                   
          </asp:GridView>
          <asp:SqlDataSource ID="sqlds1" runat="server" ConnectionString="<%$connectionstrings:DBCon %>" SelectCommand="select EMP_FIRST_NAME+' '+EMP_LAST_NAME as EMP_FIRST_NAME,Emp_Photo from YANTRA_EMPLOYEE_MAST where DATEPART(M,EMP_DOB)=DATEPART(m,getdate()) and DATEPART(d,EMP_DOB)=DATEPART(d,getdate())  and STATUS=1" SelectCommandType="Text"></asp:SqlDataSource>
      
        
        
         <p class='b_text' style="text-align:center">Best Wishes on your Birthday!
          <br />You are an extremely dedicated employee
          <br />We wish for nothing but the best in all your endeavors.
      </p>
        
        <p class='b_text'>Regards,</p>
        <%--<p class='b_text'>Narinder Anand</p>--%>
                          <div class='b_button'><span><img src="Valueline-footer-logo-1.png" style="width: 200px;height: 60px;margin-top: -35px;"/></span></div>
   
    </div>

  
      <div class ="back">


         

       <table style="width: 762px; height: 382px;" cellpadding="2">
<tbody>
<tr>


<td style="width: 80%;">

    <asp:GridView ID="gvwishes" AllowPaging ="True" PageSize ="10"  
               DataSourceID="dswishes" runat="server" AutoGenerateColumns="False" AllowSorting="True" EnableTheming="False" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Large"  >
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="pagination" VerticalAlign="Middle" ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <Columns >
                   <%-- <asp:BoundField DataField="dt_added"  HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />--%>
                    <asp:BoundField DataField="logdesc" HeaderText="Wishes"  />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>

</td>



</tr>



    <tr>
        <td style="text-align: center;">
    <asp:TextBox ID="txtwishes" TextMode ="MultiLine"  runat="server" Width="70%" placeholder="Please Type your wishes here" ></asp:TextBox><br /><br />
            <asp:Button ID="btnwishme"  Text="Wish Here" CssClass="button"  runat="server" OnClick="btnwishme_Click"    /> 

</td>

    </tr>



</tbody>
</table>





            <div >
               <%-- <asp:ImageButton style="margin-left:100px"  ID="imgbtnnotify"  runat="server" Height="25px" Width="30px" ImageUrl="~/Images/sacasc.png"  />--%>
            <asp:Label  ID="lblnotify"  Visible="false"  ForeColor="Red" runat="server"></asp:Label>
                <br />

           

     <asp:SqlDataSource ID="dswishes" runat="server" ConnectionString="<%$connectionstrings:DBCon %>" SelectCommand="select * from log_details_tbl1 where logcateid =140 and DATEPART(M,dt_added )=DATEPART(m,getdate()) and DATEPART(d,dt_added )=DATEPART(d,getdate()) order by dt_added desc" SelectCommandType="Text"></asp:SqlDataSource>

            </div>
    <%--  <div style="margin-top:95px; text-align :right ">
            <asp:TextBox ID="txtwishes" TextMode ="MultiLine"  runat="server" BorderStyle="Inset" ></asp:TextBox>
            <asp:Button ID="btnwishme"  Text="Wish Here"  runat="server" OnClick="btnwishme_Click"    /> 
            </div>--%>
      
      </div>
  </div>

  <div class='r_wrap'>

    <div class='b_round'></div>
    <div class='s_round'>
      <div class='s_arrow'></div>
    </div>

      
  </div>


  <div style="/*! margin-top: 150px; */position: absolute;bottom: -100px;right: -1px;color: white;font-size: 18px;">Click to turn</div>


</div>
    </div>
    </form>
</body>
</html>
