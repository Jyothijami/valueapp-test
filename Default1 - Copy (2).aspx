<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1 - Copy (2).aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>|| Value App : Login ||</title>   
<%--    <link href="App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        var totalCount = 5;
        function ChangeIt() {
            var num = Math.ceil(Math.random() * totalCount);
            document.body.background = 'images2/' + num + '.jpg';
            document.body.style.backgroundRepeat = "repeat";// Background repeat
        }
</script>




    <%-- Meery Chris --%>

     <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.4.min.js"></script>
    <script type="text/javascript"  src="Santa Riding Reindeer jQuery Animation/js/snowfall.jquery.min.js"></script>

    <!-- Bootstrap -->
    <link type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" rel="stylesheet"/>

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->



    <style type="text/css" media="screen">
    body{
        font-size: 18px;
        background: #000;
    }
    .well{
        background: #2C1A33;
color: #965B5B;
    }
    .santa {
        position: fixed;
        bottom: 10px;
        right: -500px;
    }
    .xmas-tree {
        position: fixed;
        bottom: -20px;
        right: 5px;
    }
    </style>
    <style type ="text/css">
       

.lightrope {
  text-align: center;
  white-space: nowrap;
  overflow: hidden;
  position: absolute;
  z-index: 1;
  margin: -15px 0 0 0;
  padding: 0;
  pointer-events: none;
  width: 100%;
}
.lightrope li {
  position: relative;
  -webkit-animation-fill-mode: both;
          animation-fill-mode: both;
  -webkit-animation-iteration-count: infinite;
          animation-iteration-count: infinite;
  list-style: none;
  margin: 0;
  padding: 0;
  display: block;
  width: 12px;
  height: 28px;
  border-radius: 50%;
  margin: 20px;
  display: inline-block;
  background: #00f7a5;
  box-shadow: 0px 4.6666666667px 24px 3px #00f7a5;
  -webkit-animation-name: flash-1;
          animation-name: flash-1;
  -webkit-animation-duration: 2s;
          animation-duration: 2s;
}
.lightrope li:nth-child(2n+1) {
  background: cyan;
  box-shadow: 0px 4.6666666667px 24px 3px rgba(0, 255, 255, 0.5);
  -webkit-animation-name: flash-2;
          animation-name: flash-2;
  -webkit-animation-duration: 0.4s;
          animation-duration: 0.4s;
}
.lightrope li:nth-child(4n+2) {
  background: #f70094;
  box-shadow: 0px 4.6666666667px 24px 3px #f70094;
  -webkit-animation-name: flash-3;
          animation-name: flash-3;
  -webkit-animation-duration: 1.1s;
          animation-duration: 1.1s;
}
.lightrope li:nth-child(odd) {
  -webkit-animation-duration: 1.8s;
          animation-duration: 1.8s;
}
.lightrope li:nth-child(3n+1) {
  -webkit-animation-duration: 1.4s;
          animation-duration: 1.4s;
}
.lightrope li:before {
  content: "";
  position: absolute;
  background: #222;
  width: 10px;
  height: 9.3333333333px;
  border-radius: 3px;
  top: -4.6666666667px;
  left: 1px;
}
.lightrope li:after {
  content: "";
  top: -14px;
  left: 9px;
  position: absolute;
  width: 52px;
  height: 18.6666666667px;
  border-bottom: solid #222 2px;
  border-radius: 50%;
}
.lightrope li:last-child:after {
  content: none;
}
.lightrope li:first-child {
  margin-left: -40px;
}

@-webkit-keyframes flash-1 {
  0%, 100% {
    background: #00f7a5;
    box-shadow: 0px 4.6666666667px 24px 3px #00f7a5;
  }
  50% {
    background: rgba(0, 247, 165, 0.4);
    box-shadow: 0px 4.6666666667px 24px 3px rgba(0, 247, 165, 0.2);
  }
}

@keyframes flash-1 {
  0%, 100% {
    background: #00f7a5;
    box-shadow: 0px 4.6666666667px 24px 3px #00f7a5;
  }
  50% {
    background: rgba(0, 247, 165, 0.4);
    box-shadow: 0px 4.6666666667px 24px 3px rgba(0, 247, 165, 0.2);
  }
}
@-webkit-keyframes flash-2 {
  0%, 100% {
    background: cyan;
    box-shadow: 0px 4.6666666667px 24px 3px cyan;
  }
  50% {
    background: rgba(0, 255, 255, 0.4);
    box-shadow: 0px 4.6666666667px 24px 3px rgba(0, 255, 255, 0.2);
  }
}
@keyframes flash-2 {
  0%, 100% {
    background: cyan;
    box-shadow: 0px 4.6666666667px 24px 3px cyan;
  }
  50% {
    background: rgba(0, 255, 255, 0.4);
    box-shadow: 0px 4.6666666667px 24px 3px rgba(0, 255, 255, 0.2);
  }
}
@-webkit-keyframes flash-3 {
  0%, 100% {
    background: #f70094;
    box-shadow: 0px 4.6666666667px 24px 3px #f70094;
  }
  50% {
    background: rgba(247, 0, 148, 0.4);
    box-shadow: 0px 4.6666666667px 24px 3px rgba(247, 0, 148, 0.2);
  }
}
@keyframes flash-3 {
  0%, 100% {
    background: #f70094;
    box-shadow: 0px 4.6666666667px 24px 3px #f70094;
  }
  50% {
    background: rgba(247, 0, 148, 0.4);
    box-shadow: 0px 4.6666666667px 24px 3px rgba(247, 0, 148, 0.2);
  }
}
    </style>

</head>
<body style="background-color:#e4e4e4">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="well">

      <div style="width:400px; margin:0 auto;">
           <script type="text/javascript">
               ChangeIt();
</script>
        <table border="0" cellpadding="5" cellspacing="5" style="width:100%">
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label2" runat="server" Text="User Name"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please Enter User Name" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="Label1" runat="server" Text="Password"></asp:Label></td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                            </td>
                            <td>
                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                &nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CausesValidation="False" UseSubmitBehavior="False" OnClick="btnReset_Click" /></td>
                        </tr>
                        <tr>
                            <td style="height: 14px; text-align: left">
                                </td>
                            <td style="padding-right: 28px; height: 14px; text-align: right">
                               </td>
                        </tr>
                    </table>
          </div>
   <ul class="lightrope">
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
  <li></li>
</ul>
        </div>


        <%-- Chrismas --%>

        <div class="santa"><img src="Santa Riding Reindeer jQuery Animation/images/christmas-sled-source_ulp.gif" alt=""></div>
        <div class="xmas-tree"><img src="Santa Riding Reindeer jQuery Animation/images/Animated_Xmas-tree-animation.gif" alt=""></div>


    <script type="text/javascript">
        $(document).ready(function () {

            var windowWidth = $(document).width();
            var santa = $('.santa');
            santa_right_pos = windowWidth + santa.width();
            santa.right = santa_right_pos;

            $(document).snowfall({ flakeCount: 150, maxSpeed: 5, maxSize: 6 });

            function movesanta() {
                santa.animate({ right: windowWidth + santa.width() }, 15000, function () {
                    santa.css("right", "-500px");
                    setTimeout(function () {
                        movesanta();
                    }, 10000);
                });
            }
            movesanta();
        });
    </script>

    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>



    </form>
</body>
</html>
