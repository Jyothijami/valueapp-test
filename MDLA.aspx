<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MDLA.aspx.cs" Inherits="LeaveApprove" %>
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
        border-style: none;
            border-color: inherit;
            border-width: 0;
            padding: 0;
            box-sizing: border-box;
            margin-left: 0;
            margin-right: 0;
            margin-bottom: 0;
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
  font-size: 0.5em;
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
    <p>Approve Leave</p>
    <span class='arrow entypo-resize-full'></span>
        </div>
        <div id='content'>
            <div id='right'>
                
        <br />
        <p style=" font:bolder; font-size :x-large; color:#ffdd33">Leave Details - <asp:Label ID="lblEMpName" runat="server" Text="Label"></asp:Label></p>
        <p style="text-align:left; font:bolder;">Leave Id : 
         <asp:Label ID="lblLeaveId" runat="server" Text="Label"></asp:Label></p>
      
        <p style="text-align:left; font:bolder;">From Date : <asp:Label ID="lblFrmDt" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp;&nbsp; To Date : <asp:Label ID="lblToDt" runat ="server" Text ="Label"></asp:Label>
        </p>
        <p style="text-align:left; font:bolder; ">Type of Applied Leave : <asp:Label ID="lblLeaveType" runat="server" Text="Label"></asp:Label>
            No Of Days Applied : <asp:Label ID="NoOfdays" runat="server" Text="Label"></asp:Label>
        </p>

        <p style="text-align:left; font:bolder;  ">Reason for Leave : <asp:Label ID="lblReason" runat="server" Text="Label"></asp:Label></p>
        <p style="text-align:left; font:bolder;  ">Address in Leave Period : <asp:Label ID="lblAdd" runat="server" Text="Label"></asp:Label></p>
             <br />
                <p><asp:Button ID="btnApprove" runat ="server" Text="Approve"  OnClick ="btnHODApprove_Click" />

                     &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReject" runat ="server" Text ="Reject"  OnClick ="btnHODReject_Click" />

                </p>   
                <p><asp:Label ID="lblDeptId" runat ="server" Visible ="false"  ></asp:Label>
                    <asp:Label ID="lblEmpIdHidden" runat ="server" Visible ="false" ></asp:Label>
                    <asp:Label ID="EmpId" runat ="server" Visible ="false" ></asp:Label>

                    <asp:Label ID="lblUserType" runat ="server" Visible ="false" ></asp:Label>
                    <asp:Label ID="lblHOD_Id" runat ="server" Visible ="false" ></asp:Label>
                    <asp:Label ID="lblMD_Name" runat ="server" Visible ="false" ></asp:Label>
                    <asp:Label ID="lblMobile" runat ="server" Visible ="false" ></asp:Label>
                </p>
    </div>
  </div>
</div>

    </div>
    </form>
</body>
</html>