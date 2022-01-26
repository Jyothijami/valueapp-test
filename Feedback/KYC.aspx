<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KYC.aspx.cs" Inherits="Feedback_KYC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
   
    <head runat="server">
         <style>
        @-webkit-keyframes 
cardEnter {  0%, 20%, 40%, 60%, 80%, 100% {
 -webkit-transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
 transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
}
 0% {
 opacity: 0;
 -webkit-transform: scale3d(0.3, 0.3, 0.3);
}
 20% {
 -webkit-transform: scale3d(1.1, 1.1, 1.1);
}
 40% {
 -webkit-transform: scale3d(0.9, 0.9, 0.9);
}
 60% {
 opacity: 1;
 -webkit-transform: scale3d(1.03, 1.03, 1.03);
}
 80% {
 -webkit-transform: scale3d(0.97, 0.97, 0.97);
}
 100% {
 opacity: 1;
 -webkit-transform: scale3d(1, 1, 1);
}
}
@keyframes 
cardEnter {  0%, 20%, 40%, 60%, 80%, 100% {
 -webkit-transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
 transition-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
}
 0% {
 opacity: 0;
 -webkit-transform: scale3d(0.3, 0.3, 0.3);
 transform: scale3d(0.3, 0.3, 0.3);
}
 20% {
 -webkit-transform: scale3d(1.1, 1.1, 1.1);
 transform: scale3d(1.1, 1.1, 1.1);
}
 40% {
 -webkit-transform: scale3d(0.9, 0.9, 0.9);
 transform: scale3d(0.9, 0.9, 0.9);
}
 60% {
 opacity: 1;
 -webkit-transform: scale3d(1.03, 1.03, 1.03);
 transform: scale3d(1.03, 1.03, 1.03);
}
 80% {
 -webkit-transform: scale3d(0.97, 0.97, 0.97);
 transform: scale3d(0.97, 0.97, 0.97);
}
 100% {
 opacity: 1;
 -webkit-transform: scale3d(1, 1, 1);
 transform: scale3d(1, 1, 1);
}
}
.RadioIndentFor {
  display: inline-block;
  padding-right: 20px;
  font-size: 18px;
  line-height: 49px;
  cursor: pointer;
}

.RadioIndentFor:hover .inner {
  -webkit-transform: scale(0.5);
  -ms-transform: scale(0.5);
  transform: scale(0.5);
  opacity: .5;
}

.RadioIndentFor input {
  width: 5px;
  height: 5px;
  opacity: 0;
}

.RadioIndentFor input:checked + .outer .inner {
  -webkit-transform: scale(1);
  -ms-transform: scale(1);
  transform: scale(1);
  opacity: 1;
}

.RadioIndentFor input:checked + .outer { border: 3px solid #f08b3b; }

.radio input:focus + .outer .inner {
  -webkit-transform: scale(1);
  -ms-transform: scale(1);
  transform: scale(1);
  opacity: 1;
  background-color: #e67012;
}

.RadioIndentFor .outer {
  width: 20px;
  height: 20px;
  display: block;
  float: left;
  margin: 10px 9px 10px 10px;
  border: 3px solid #0c70b4;
  border-radius: 50%;
  background-color: #fff;
}

.RadioIndentFor .inner {
  /*-webkit-transition: all 0.25s ease-in-out;
  transition: all 0.25s ease-in-out;*/
  width: 16px;
  height: 16px;
  -webkit-transform: scale(0);
  -ms-transform: scale(0);
  transform: scale(0);
  display: block;
  margin: 2px;
  border-radius: 50%;
  background-color: #f08b3b;
  opacity: 0;
}
    </style>
         
    <meta charset="utf-8" />
       <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="author" content="colorlib.com" />
        <title>Customer Feedback Form</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" href="form.css" />
        <%--<meta name="viewport" content="width=device-width, initial-scale=1" />--%>
</head>

<body>
    <form id="form1" runat="server">
    <div class="container">
            <div class="span7">
                <%--<marquee direction="scroll" style="height: 25px">--%>
               <%-- <ul>--%>
                    <a style ="color:white; text-align :right  " href="http://Valueline.in/" target="_blank">www.valueline.in</a>
                <%--</ul>--%>
                    <%--</marquee>--%>
            </div>
            <%--<div class="container-box rotated">
                <button type="button" class="btn btn-info btn-lg turned-button" data-toggle="modal" data-target="#myModal">Rating Us</button>
            </div>--%>
            <!-- Modal -->
           <%-- <div id="myModal" class="modal fade" role="dialog">--%>
                <div class="modal-dialog modal-full-height modal-right modal-notify modal-info" role="document">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header" role="dialog"  data-dismiss="modal">
                            <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">
                                 Feedback Submitted on <asp:Label ID="lblDt" runat ="server" ></asp:Label>
                            </h4>
                        </div>
                        <div <%--style ="background-color:#F5F5F5"--%> class="modal-body bg-danger">
                            <%--<form role="form" method="post" id="reused_form">--%>
                                 <table>
                                     <tr>
                                         <td colspan ="3"><asp:Label runat ="server" ID="lbl1" Font-Bold ="true"  Text="1. Did you find what you were looking for?" ></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; padding-left: 10px;" colspan ="3">
                                             
                                            <asp:RadioButtonList ID="rdblIndentfor" runat="server" AutoPostBack="True"  RepeatDirection="Vertical" RepeatLayout="Flow" OnSelectedIndexChanged="rdblIndentfor_SelectedIndexChanged">
                                                 <asp:ListItem >Yes</asp:ListItem>
                                                 <asp:ListItem>No</asp:ListItem>
                                                 <%--<asp:ListItem >Yes, but price is high</asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                                 </td>
                                     </tr>
                                     <tr>
                                         <td colspan ="3" style="text-align: left; padding-left: 10px;" ><asp:Label ID="lblNoText" Visible ="false"  runat ="server" Text="Please let us know what you were looking for"></asp:Label>
                                             <br />
                                             <asp:TextBox ID="txtNo" TextMode ="MultiLine" Visible ="false"   runat ="server"></asp:TextBox>
                                         </td>
                                     </tr>
                                     <tr> 
                                        
                                         <td colspan ="3" style="text-align: left; padding-left: 10px;" ><asp:Label ID="lblYPricehigh" Visible ="false"  runat ="server" Text="Please mention the product name and your price range below"></asp:Label>
                                             <br />
                                             <asp:TextBox ID="txtYPricehigh" TextMode ="MultiLine" Visible ="false"   runat ="server"></asp:TextBox>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td colspan ="3"><asp:Label runat ="server" Font-Bold ="true" ID="lbl2" Text="2. Please rate our sales representative on the following " ></asp:Label></td>
                                     </tr>
                                      <tr>
                                          
                                         <td colspan="3"><asp:Label runat ="server" ID="lbl3" Text="Behaviour" Font-Bold="True" ></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; padding-left: 10px;" colspan ="3">
                                            <asp:RadioButtonList ID="rdb2"  runat="server" AutoPostBack="True"  RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdb2_SelectedIndexChanged">
                                                 <asp:ListItem >Good</asp:ListItem>
                                                 <asp:ListItem>Bad</asp:ListItem>
                                                 
                                            </asp:RadioButtonList></td>
                                     </tr>
                                     <tr> 
                                         
                                         <td colspan ="3" style="text-align: left; padding-left: 10px;" ><asp:Label ID="lblBad" Visible ="false"  runat ="server" Text="Please let us know what went wrong"></asp:Label>
                                             <br />
                                             <asp:TextBox ID="txtbad" TextMode ="MultiLine" Visible ="false"   runat ="server"></asp:TextBox>
                                         </td>
                                     </tr>
                                     <tr>
                                         
                                         <td colspan ="3"><asp:Label runat ="server" ID="lbl4" Text="Knowledge" Font-Bold="True" ></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; padding-left: 10px;" colspan ="3">
                                            <asp:RadioButtonList ID="rdb3"  runat="server" AutoPostBack="True"  RepeatDirection="Horizontal" RepeatLayout="Flow" >
                                                 <asp:ListItem >Good</asp:ListItem>
                                                 <asp:ListItem>Average</asp:ListItem>
                                                 <asp:ListItem>Bad</asp:ListItem>
                                                 
                                            </asp:RadioButtonList></td>
                                     </tr>
                                     <tr>
                                         <td><br /></td>
                                     </tr>
                                      <tr>
                                         <td colspan ="3"><asp:Label runat ="server" Font-Bold ="true" ID="lbl5" Text="3. What do you feel about our showroom ambience? " ></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; padding-left: 10px;" colspan ="3">
                                            <asp:RadioButtonList ID="rdb4"  runat="server" AutoPostBack="True"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <asp:ListItem >Excellent</asp:ListItem>
                                                 <asp:ListItem >Good</asp:ListItem>
                                                 <asp:ListItem>Average</asp:ListItem>
                                                 <asp:ListItem>Bad</asp:ListItem>
                                                 
                                            </asp:RadioButtonList></td>
                                     </tr>
                                     <tr>
                                         <td><br /></td>
                                     </tr>
                                     <tr>
                                         <td colspan ="3"><asp:Label runat ="server" Font-Bold ="true" ID="lbl6" Text="4. Would you like to visit us again for future requirement? " ></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; padding-left: 10px;" colspan ="3">
                                            <asp:RadioButtonList ID="rdb5"  runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                
                                                 <asp:ListItem >Yes</asp:ListItem>
                                                 <asp:ListItem>Maybe</asp:ListItem>
                                                 <asp:ListItem>No</asp:ListItem>
                                                 
                                            </asp:RadioButtonList></td>
                                     </tr>
                                     <tr>
                                         <td><br /></td>
                                     </tr>
                                     <tr>
                                         <td colspan ="3"><asp:Label runat ="server" Font-Bold ="true" ID="lbl7" Text="5. How Satisfied are you with our service? " ></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td style="text-align: left; padding-left: 10px;" colspan ="3">
                                            <asp:RadioButtonList ID="rdb6"  runat="server" AutoPostBack="True"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                
                                                <asp:ListItem Value ="5" >Excellent</asp:ListItem>
                                                 <asp:ListItem Value ="4" >Good</asp:ListItem>
                                                 <asp:ListItem Value ="3">Average</asp:ListItem>
                                                 <asp:ListItem Value ="2">Bad</asp:ListItem>
                                                 <asp:ListItem Value ="1">Very Bad</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                     </tr>
                                     <tr>
                                         <td><br /></td>
                                     </tr>
                                     <tr>
                                         <td colspan ="3"><asp:Label runat ="server" Font-Bold ="true" ID="lbl8" Text="6. If you have anay other comments please mention below " ></asp:Label></td>
                                     </tr>
                                     <tr >

                                         <td  style="text-align: left; padding-left: 10px;" colspan ="3"><asp:TextBox ID="txtcomments" runat ="server" TextMode ="MultiLine"  ></asp:TextBox></td>
                                     </tr>
                                     <tr>
                                         <td><asp:TextBox ID="txtCustName" runat ="server" ></asp:TextBox></td>
                                         <td><asp:TextBox ID="txtCustMobile" runat ="server" ></asp:TextBox></td>
                                         
                                     </tr>
                                     <tr><td><asp:TextBox ID ="txtCustEmail" TextMode ="MultiLine"  runat ="server" ></asp:TextBox></td></tr>
                                     <tr>
                                        <td style="text-align: left">
                <asp:Button ID="btnSubmit" runat="server" Visible ="false"  Text="Submit" BackColor="#66FF66" OnClick="btnSubmit_Click" Style="font-weight: 700" />&nbsp;
            </td>
                                         <td>
                <asp:Button ID="btnCancel" runat="server" Visible ="false"  Text="Cancel" BackColor="#FF6666"  />

                                         </td>
                                     </tr>
                                 </table>
                            <%--</form>--%>
                           
                        </div>
                    </div>
                </div>
            <%--</div>--%>

        </div>
    </form>
</body>
</html>
