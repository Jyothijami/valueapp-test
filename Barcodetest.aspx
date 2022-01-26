<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Barcodetest.aspx.cs" Inherits="Barcodetest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
         <asp:TextBox ID="txtCode" runat="server"></asp:TextBox> <br />
          <asp:TextBox ID="txt2" runat="server"></asp:TextBox> <br />
          <asp:TextBox ID="txt3" runat="server"></asp:TextBox> <br />
          <asp:TextBox ID="txt4" runat="server"></asp:TextBox> <br />



    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" />
    <hr />
    <asp:PlaceHolder ID="plBarCode" runat="server" />


      
    </div>
    </form>
</body>
</html>
