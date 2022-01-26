<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wastelogin.aspx.cs" Inherits="wastelogin" %>

<!DOCTYPE html>

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
</head>
<body style="background-color:#e4e4e4">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    </form>
</body></html>
