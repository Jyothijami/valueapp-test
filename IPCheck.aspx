<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IPCheck.aspx.cs" Inherits="IPCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>|| Value App||</title>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br />
    <table align="center">
    <tr>
        <td>
            Enter OTP :
            <asp:TextBox ID="txtOtp" runat="server"></asp:TextBox>
            <asp:Label ID="lblUserName" runat ="server" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: center">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
        </td>
    </tr>
        <tr>
            <td>
                <asp:Label ID="lblUserOtp" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblOtp" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="Please Wait to receive Your OTP(Don't try to Login Again)" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
</table>
    </div>
    </form>
</body>
</html>
