<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="initial-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <link rel="icon" type="image/ico" href="favicon.ico"/>
    <title>Valueline - Login</title>
    <link rel="stylesheet" href="css/login.css"/>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans+Condensed:300' rel='stylesheet'/>
    <!-- jQuery framework -->
        <script src="js/jquery.min.js"></script>
       <script src="js/jquery.validate.js"></script>

    <script type="text/javascript">
        (function (a) { a.fn.vAlign = function () { return this.each(function () { var b = a(this).height(), c = a(this).outerHeight(), b = (b + (c - b)) / 2; a(this).css("margin-top", "-" + b + "px"); a(this).css("top", "50%"); a(this).css("position", "absolute") }) } })(jQuery); (function (a) { a.fn.hAlign = function () { return this.each(function () { var b = a(this).width(), c = a(this).outerWidth(), b = (b + (c - b)) / 2; a(this).css("margin-left", "-" + b + "px"); a(this).css("left", "50%"); a(this).css("position", "absolute") }) } })(jQuery);
        $(document).ready(function () {
            if ($('#login-wrapper').length) {
                $("#login-wrapper").vAlign().hAlign()
            };
            if ($('#login-validate').length) {
                $('#login-validate').validate({
                    onkeyup: false,
                    errorClass: 'error',
                    rules: {
                        login_name: { required: true },
                        login_password: { required: true }
                    }
                })
            }
            if ($('#forgot-validate').length) {
                $('#forgot-validate').validate({
                    onkeyup: false,
                    errorClass: 'error',
                    rules: {
                        forgot_email: { required: true, email: true }
                    }
                })
            }
            $('#pass_login').click(function () {
                $('.panel:visible').slideUp('200', function () {
                    $('.panel').not($(this)).slideDown('200');
                });
                $(this).children('span').toggle();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="login-wrapper" class="clearfix" style="margin-top: -137.5px; top: 50%; position: absolute; margin-left: -248px; left: 50%;">
        <div class="main-col">
            <img src="Images/vlinenewlogo.jpeg" alt="" class="logo_img" />
            <div class="panel">
                <p class="heading_main">Account Login</p>
              
                    <label for="login_name">Login</label>
                                    <asp:TextBox ID="txtUserName" runat="server" Placeholder="UserName" ></asp:TextBox>
                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please Enter User Name" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                    <label for="login_password">Password</label>
                   <asp:TextBox ID="txtPassword" TextMode="Password" Placeholder="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please Enter Password" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                   
                    <div class="submit_sect">
                       <asp:Button ID="Button1" runat="server" Text="Login" OnClick="btnLogin_Click" />
                    </div>
               <asp:Label ID="lblip" runat="server"></asp:Label>
            </div>
            
        </div>
       
    </div>
    </form>
</body>
</html>
