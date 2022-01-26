<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List_eQuotations.aspx.cs" Inherits="Modules_SM_List_eQuotations" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 233px;
            height: 77px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnGenerateQuot1" runat="server" OnClick="btnGenerateQuot1_Click" Text="Generate Quotation" />
    
    </div>
    </form>
    <p>
        <img alt="" class="auto-style2" src="../../Images/small_logo.tif" /></p>
</body>
</html>

 
