<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1 - Copy.aspx.cs" Inherits="_Default" %>

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





    <meta charset="UTF-8"/>
	<meta name="viewport" content="width=device-width, initial-scale=1"/>
<!--===============================================================================================-->	
	<link rel="icon" type="image/png" href="images/icons/favicon.ico"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/vendor/bootstrap/css/bootstrap.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/fonts/font-awesome-4.7.0/css/font-awesome.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/fonts/Linearicons-Free-v1.0.0/icon-font.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/vendor/animate/animate.css"/>
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="Login_v10/vendor/css-hamburgers/hamburgers.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/vendor/animsition/css/animsition.min.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/vendor/select2/select2.min.css"/>
<!--===============================================================================================-->	
	<link rel="stylesheet" type="text/css" href="Login_v10/vendor/daterangepicker/daterangepicker.css"/>
<!--===============================================================================================-->
	<link rel="stylesheet" type="text/css" href="Login_v10/css/util.css"/>
	<link rel="stylesheet" type="text/css" href="Login_v10/css/main.css"/>
<!--===============================================================================================-->


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


    

</head>
<body >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
         <div class="well">

        <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100 p-t-50 p-b-90">
				<div class="login100-form validate-form flex-sb flex-w">
					<span class="login100-form-title p-b-51">
						Login
					</span>

					
					<div class="wrap-input100 validate-input m-b-16" data-validate = "Username is required">
						 <asp:TextBox ID="txtUserName" CssClass="input100" placeholder="UserName" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please Enter User Name" SetFocusOnError="True">*</asp:RequiredFieldValidator>

                        
                      <%--  <input class="input100" type="text" name="username" placeholder="Username">--%>
						<span class="focus-input100"></span>
					</div>
					
					
					<div class="wrap-input100 validate-input m-b-16" data-validate = "Password is required">
						
                           <asp:TextBox ID="txtPassword" CssClass="input100" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        
                        
                      <%--  <input class="input100" type="password" name="pass" placeholder="Password">--%>
						<span class="focus-input100"></span>
					</div>
					
				

					<div class="">

                         <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-danger" runat="server" Text="Login" OnClick="btnLogin_Click" />

					</div>

				</div>
			</div>
		</div>
	</div>
	
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

            $(document).snowfall({ flakeCount: 100, maxSpeed: 5, maxSize: 5 });

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


















	<div id="dropDownSelect1"></div>
	
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/vendor/bootstrap/js/popper.js"></script>
	<script type="text/javascript" src="Login_v10/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/vendor/daterangepicker/moment.min.js"></script>
	<script type="text/javascript" src="Login_v10/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
	<script type="text/javascript" src="Login_v10/js/main.js"></script>

     
       

     </form>
</body>
</html>
