<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustRating.aspx.cs" Inherits="Feedback_SrvRating" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">


        @import url(https://fonts.googleapis.com/css?family=Open+Sans:100,300,400,600);
@import url(http://weloveiconfonts.com/api/?family=brandico|entypo);
/* entypo */
[class*="entypo-"]:before {
  font-family: 'entypo', sans-serif;
  padding-right: 10px;
}

body {
  font-family: 'Open Sans', sans-serif;
  font-size: 100%;
  background: #829292;
}

*, *:before, *:after {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  border: 0;
}

ul {
  list-style: none;
}

a {
  color: white;
  text-decoration: none;
}

#browser {
  margin: 10px auto;
  color: white;
  width: 90%;
  
  -webkit-box-shadow: 2px 5px 15px rgba(0, 0, 0, 0.5);
  -moz-box-shadow: 2px 5px 15px rgba(0, 0, 0, 0.5);
  box-shadow: 2px 5px 15px rgba(0, 0, 0, 0.5);
}

#browser-bar {
  background: #394141;
  width: 100%;
  
  padding: 8px;
  margin: 0 auto;
  font-weight: 300;
  font-size: 0.9em;
  position: relative;
}
#browser-bar p {
  text-align: center;
}
#browser-bar .circles {
  border-radius: 10px;
  height: 13px;
  width: 13px;
  background: #ff3434;
  float: left;
  margin-left: 7px;
  margin-top: 3px;
}
#browser-bar .circles:nth-of-type(2) {
  background: #ffdd33;
}
#browser-bar .circles:nth-of-type(3) {
  background: #33FF00;
}

.arrow {
  position: absolute;
  right: 3px;
  top: 6px;
  font-size: 1.2em;
  color: #8e9699;
}

#content {
  background: #454f4f;
  width: 100%;
}
.container{
    
   display:inline-block;
  position: relative;
  padding-left: 20px;
  margin-bottom: 12px;
  cursor: pointer;
  font-size: 100%;
     margin-left: 5px;
  margin-top: 3px;
  border-radius: 10px;
  margin:-2px;
  line-height :30px;
 
  
}

#content:after {
  content: "";
  display: table;
  clear: both;
}
#content #left, #content #right {
  height: 100%;
}
#content #left {
  float: left;
  width: 40%;
  height: 100%;
}
@media all and (max-width: 780px) {
  #content #left {
    width: 100%;
  }
}
#content #left #map {
  height: 400px;
  position: relative;
  /*background-image: url("http://f.cl.ly/items/452R3S1440221Z3m372j/israel.png");
  background-size: cover;*/
}
#content #left #map p {
  text-transform: uppercase;
  padding-top: 20px;
  padding-left: 30px;
  font-size: 0.9em;
  font-weight: 600;
}
#content #left #map .zoom {
  position: absolute;
  right: 25px;
  top: 25px;
  width: 2px;
  height: 70px;
  background: white;
}
#content #left #map .zoom:before, #content #left #map .zoom:after {
  text-align: center;
  font-weight: 600;
  position: absolute;
  color: #7BC087;
  background: white;
  width: 20px;
}
#content #left #map .zoom:before {
  content: '+';
  top: -10px;
  right: -8px;
}
#content #left #map .zoom:after {
  content: '-';
  bottom: -10px;
  right: -8px;
}
#content #left #map .map-locator {
  position: absolute;
  top: 40%;
  left: 30%;
  border-radius: 15px;
  height: 30px;
  width: 30px;
  background: rgba(0, 0, 0, 0.4);
  border: solid 2px white;
}
#content #left #map .map-locator:before {
  content: '';
  position: absolute;
  top: 9px;
  left: 9px;
  width: 8px;
  height: 8px;
  background: white;
  border-radius: 5px;
}
#content #left #map .map-locator .tooltip {
  position: absolute;
  color: #394141;
  left: 50px;
  top: -10px;
  background: white;
  font-size: 0.8em;
  font-weight: 600;
  -webkit-box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
  -moz-box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
  box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
}
#content #left #map .map-locator .tooltip:before {
  content: '';
  position: absolute;
  left: -10px;
  top: 14px;
  border-right: solid white 10px;
  border-top: solid transparent 8px;
  border-bottom: solid transparent 8px;
}
#content #left #map .map-locator .tooltip [class*="entypo-"] {
  min-width: 25px;
  display: inline-block;
  text-align: center;
  border-right: solid thin #CCC;
  margin-right: 5px;
}
#content #left #map .map-locator .tooltip li {
  border-bottom: solid thin #CCC;
  padding: 10px;
  white-space: nowrap;
}
#content #left #map .map-locator .tooltip li a {
  color: #51B2D6;
}
#content #left #map .map-locator .tooltip li:hover [class*="entypo-"] {
  color: #51B2D6;
}
#content #left ul#location-bar {
  width: 100%;
  text-align: center;
  display: table;
}
#content #left ul#location-bar li {
  display: table-cell;
  padding: 15px;
  background: #5FA269;
  border-right: solid thin #7fb587;
}
#content #left ul#location-bar li:last-of-type {
  border-right: 0;
}
#content #left ul#location-bar li:hover {
  background: #4c8254;
}
#content #left ul#location-bar .active {
  background: #4c8254;
}
#content #right {
  float: left;
  width: 92%;
  background: #454f4f;
  font-size: 0.99em;
  padding: 25px;
}
@media all and (max-width: 780px) {
  #content #right {
    width: 100%;
  }
}
#content #right p {
  margin-bottom: 10px;
  /*text-transform: uppercase;*/
}
#content #right a:hover {
  color: #51B2D6;
}
#content #right #social {
  display: table;
  width: 100%;
}
#content #right .social {
  display: table-cell;
  text-align: center;
}
#content #right form {
  border-top: solid thin #8e9699;
  border-bottom: solid thin #8e9699;
  margin: 20px 0;
  padding: 20px 0;
}
#content #right form input,
#content #right form textarea {
  background: #394141;
  padding: 8px;
  margin-bottom: 8px;
  width: 100%;
  color: white;
}
#content #right form input:last-of-type,
#content #right form textarea:last-of-type {
  margin-bottom: 0;
}
#content #right form input[type='submit'],
#content #right form textarea[type='submit'] {
  text-transform: uppercase;
  background: #7BC087;
  width: 50%;
  color: white;
  margin-top: 5px;
}
#content #right form input[type='submit']:hover,
#content #right form textarea[type='submit']:hover {
  background: #58b068;
  -webkit-box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
  -moz-box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
  box-shadow: 1px 1px 2px rgba(0, 0, 0, 0.5);
}
#content #right form input[type='textarea'],
#content #right form textarea[type='textarea'] {
  min-height: 75px;
  vertical-align: text-top;
}
#content #right p.other {
  font-size: 0.7;
  margin-bottom: 5px;
  text-transform: lowercase;
  font-weight: 100;
}
#content #right p.other,
#content #right p.other a {
  color: #8e9699;
}

html,body {padding:0;margin:0;}
.wrap {
  /*font:12px Arial, san-serif;*/
}





form .buttons {
  /*margin:30px 0;*/
  padding:0 4.25%;
  text-align:right
} 
form .buttons button {
  padding: 15px 20px;
  background-color: #67ab49;
  border: 0;
  border-radius: 5px;
}
form .buttons .clear {background-color: #e9e9e9;}
form .buttons .submit {background-color: #67ab49;} 
form .buttons .clear:hover {background-color: #ccc;}
form .buttons .submit:hover {background-color: #14892c;} 

        #buttons {
            text-align: center;
            line-height :30px;
            padding-bottom:15px;
            padding-top :15px;
            width:100%;
            
        }



    </style>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="https://unpkg.com/jquery"></script>
        <script src="https://surveyjs.azureedge.net/1.0.69/survey.jquery.js"></script>
        <link href="https://surveyjs.azureedge.net/1.0.69/survey.css" type="text/css" rel="stylesheet" />
        <link rel="stylesheet" href="./index.css" />
        <script src="https://unpkg.com/jquery-bar-rating"></script>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="icheck.min.js"></script>
    <link href="skins/square/red.css" rel="stylesheet" />

    <script>
        $(document).ready(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-red',
                radioClass: 'iradio_square-red',
                increaseArea: '20%' // optional
            });
        });
    </script>
    <style>
        label {
            margin-left: 5px;
            margin-right: 25px;
            
        }
        TextBox {
            height :200px;
            width :70%;
        }
        .clear {}
        .submit {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id='browser'>
        <div id='browser-bar'>
            <div class='circles'></div>
    <div class='circles'></div>
    <div class='circles'></div>
    <p  style ="font-weight :bold; font-size :large">Feedback Submitted By <asp:Label ForeColor ="#ffdd33" ID="lblCustName" runat ="server"  ></asp:Label></p>
    <p style ="font-weight :bold; font-size :large">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date : <asp:Label ForeColor ="#ffdd33" ID="lblDt" runat ="server" ></asp:Label></p>
    <p style ="font-weight :bold; font-size :large">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Attended By : <asp:Label ForeColor ="#ffdd33" ID="lblExeName" runat ="server" ></asp:Label></p>

    <span class='arrow entypo-resize-full'></span>
        </div>
        <div id='content'>
            <div id='right'>
                 <%--<p style="font:bolder;color:yellow">Please Rate Us    </p>--%>
                <p style ="font-weight :bold; color :#ffdd33 ">1. <asp:Label runat ="server" ID="Label3" Font-Bold ="true"  Text="Did you find what you were looking for?" ></asp:Label></p>
               

      <%--<p style="font:bolder;color:yellow" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1 is bad, 5 is good (i.e., "How satisfied are you with our service?")</p>--%>
      <div class="radio radiobuttonlist col-sm-9"><p>
        <asp:RadioButtonList ID="rdblIndentfor" CssClass="container" runat="server" AutoPostBack="True"  RepeatDirection="Vertical" RepeatLayout="Flow" OnSelectedIndexChanged="rdblIndentfor_SelectedIndexChanged">
                                                 <asp:ListItem >Yes</asp:ListItem>

                                                 <asp:ListItem>No</asp:ListItem>
                                                 <%--<asp:ListItem >Yes, but price is high</asp:ListItem>--%>
                                            </asp:RadioButtonList></p>
      </div>
                <p>
       <asp:Label CssClass="container" ID="lblNoText" Visible ="false" ForeColor ="#ff3434" Font-Bold ="true"    runat ="server" Text="Please let us know what you were looking for"></asp:Label></p>
                                            
       <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNo" CssClass ="container" TextMode ="MultiLine" Visible ="false"   runat ="server"></asp:TextBox>

                </p> 
                <br />
                   
                 <p style ="font-weight :bold; color :#ffdd33 ">2. <asp:Label ID="Label1" runat="server" Text="Please rate our sales representative on the following "></asp:Label></p><br />
                 <p style="font:bolder;color:yellow">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Behaviour "></asp:Label></p>
                <div class="radio radiobuttonlist col-sm-9"><p>
                     <asp:RadioButtonList ID="rdb2"  runat="server" AutoPostBack="True" CssClass="container" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rdb2_SelectedIndexChanged">
                                                 <asp:ListItem >Good</asp:ListItem>
                                                 <asp:ListItem >Average</asp:ListItem>

                                                 <asp:ListItem>Bad</asp:ListItem>
                                                 
                                            </asp:RadioButtonList></p> 
                </div>
                <p>
                    <asp:Label ID="lblBad" Visible ="false" CssClass="container"  runat ="server" ForeColor ="#ff3434" Font-Bold ="true"  Text="Please let us know what went wrong"></asp:Label>

                </p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtbad" CssClass="container" TextMode ="MultiLine" Visible ="false"   runat ="server"></asp:TextBox>
                </p>
                <%--<div class="wrap">--%>
                   <%-- <form action="">--%>
                    <p style="font:bolder;color:yellow">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Knowledge "></asp:Label></p>

    
                        <%--</form>--%> 
                <%--</div>--%>
                <div class="radio radiobuttonlist col-sm-9"><p> <asp:RadioButtonList ID="rdb3" CssClass="container" RepeatLayout="Flow" RepeatDirection="Horizontal" runat ="server" >
                    <asp:ListItem  >Good</asp:ListItem>
                    <%--<asp:ListItem Value ="2"  >Bad</asp:ListItem>--%>
                    <asp:ListItem >Average</asp:ListItem>
                    <asp:ListItem  >Poor</asp:ListItem>
                    <%--<asp:ListItem Value ="5"  >Very Good</asp:ListItem>--%>
                    </asp:RadioButtonList></p> </div> <br />
                <p style ="font-weight :bold; color :#ffdd33 ">3. <asp:Label ID="lblshow" runat="server" Text=" What do you feel about our showroom ambience? "></asp:Label></p>
                <div class="radio radiobuttonlist col-sm-9"><p> <asp:RadioButtonList ID="rdb4" AutoPostBack ="true" RepeatLayout="Flow" RepeatDirection="Horizontal"  CssClass="container"  runat ="server"  >
                     <asp:ListItem >Excellent</asp:ListItem>
                                                 <asp:ListItem >Good</asp:ListItem>
                                                 <asp:ListItem>Average</asp:ListItem>
                                                 <asp:ListItem>Bad</asp:ListItem>
                    
                    </asp:RadioButtonList> 
                   </p>
                </div> 
                 <p style ="font-weight :bold; color :#ffdd33 ">4. <asp:Label ID="Label6" runat="server" Text=" Would you like to visit us again for future requirement?"></asp:Label></p>
                 <div class="radio radiobuttonlist col-sm-9"> <p> <asp:RadioButtonList ID="rdb5" CssClass="container" RepeatLayout="Flow" RepeatDirection="Horizontal" runat ="server" >
                     <asp:ListItem >Yes</asp:ListItem>
                                                 <asp:ListItem>Maybe</asp:ListItem>
                                                 <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList></p></div>
                 <p style ="font-weight :bold; color :#ffdd33 ">5. <asp:Label ID="Label4" runat="server" Text="How Satisfied are you with our service? "></asp:Label></p>
                <div class="radio radiobuttonlist col-sm-9"><p> <asp:RadioButtonList ID="rdb6" AutoPostBack ="true" RepeatLayout="Flow" RepeatDirection="Horizontal"  CssClass="container"  runat ="server">
                     <asp:ListItem Value ="5" >Excellent</asp:ListItem>
                                                 <asp:ListItem Value ="4" >Good</asp:ListItem>
                                                 <asp:ListItem Value ="3">Average</asp:ListItem>
                                                 <asp:ListItem Value ="2">Bad</asp:ListItem>
                                                 <asp:ListItem Value ="1">Very Bad</asp:ListItem>
                    </asp:RadioButtonList> 
                   </p>
                </div> 
                <p style="font:bolder;color:yellow">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="Client Comments"></asp:Label></p>
                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox  ID="txtcomments" CssClass="container" TextMode ="MultiLine"   runat ="server"></asp:TextBox></p>
             
            </div>
            <div id="surveyElement"></div>
                                    <div id="surveyResult"></div>

                                    <script type="text/javascript" src="./index.js"></script>
            
        </div>
        
        </div> 
        </div> 

  

    </form>
</body>
</html>
