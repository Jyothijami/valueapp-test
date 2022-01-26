<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FPSLogin.aspx.cs" Inherits="FPSLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1" />
    <title>FPS :: Login </title>

    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/londinium-theme.css" rel="stylesheet" type="text/css" />
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
       <link href="css/icons.css" rel="stylesheet" type="text/css" />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&amp;subset=latin,cyrillic-ext" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <!-- Navbar -->
        <div class="navbar navbar-inverse" role="navigation">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-right">
                    <span class="sr-only">Toggle navbar</span>
                    <i class="icon-grid3"></i>
                </button>
                <a class="navbar-brand" href="Index.aspx">
                    <img src="images/logo.png" alt="Valueline" /></a>
            </div>
        </div>
        <!-- /navbar -->
      





         <!-- Login wrapper -->
        <div class="login-wrapper">

            <div class="popup-header">
                <a href="#" class="pull-left"><i class="icon-user-plus"></i></a>
                <span class="text-semibold">User Login</span>
            </div>
            <div class="well">
                <div class="form-group has-feedback">
                    <label>Username</label>
                    <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Username" />
                    <i class="icon-users form-control-feedback"></i>
                </div>
                <asp:Label ID="lblotpmsg" Visible ="false"  runat="server" Text="Please Wait to receive Your OTP(Don't try to send OTP Again)" ForeColor="#FF3300"></asp:Label>
                <asp:Label ID="lblUserOtp" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblOtp" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblMessage" runat="server" Visible="False"></asp:Label>

                <div class="form-group has-feedback">

                    <label id ="lblOtp1" runat ="server" visible ="false" >OTP</label>
                    <asp:TextBox ID="txtPassword" Visible ="false"  runat="server" class="form-control" TextMode="Number"  placeholder="OTP" />
                    <i class="icon-lock form-control-feedback"></i>
                </div>

                <div class="row form-actions">
                    <div class="col-xs-6">
                       <%-- <li><a href="#">Forgot password?</a></li>--%>
                    </div>

                    <div class="col-xs-6">

                    <asp:Button ID="btnsignin" CssClass="btn btn-warning pull-right" runat="server" Text="Sign in" OnClick="btnsignin_Click" />
                        <asp:Button ID="btnSubmit" Visible ="false" CssClass="btn btn-success pull-right" runat="server" Text="Sign in" OnClick ="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
        <!-- /login wrapper -->
















        <!-- Footer -->
        <div class="footer clearfix">
            <div class="pull-left">&copy; 2021 Valueline. All Rights Reserved</div>
            <div class="pull-right icons-group">
                <a href="Valuelineapp.com"></a>
            </div>
        </div>
        <!-- /footer -->
    </form>
</body>
</html>
