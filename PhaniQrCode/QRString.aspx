<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRString.aspx.cs" Inherits="PhaniQrCode_QRString" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtQr" runat ="server" OnTextChanged ="txtQr_TextChanged" ></asp:TextBox>
    </div>
    </form>
</body>
</html>
