<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="eloi_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>Enter passcode :</td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Connection String :</td>
                            <td>
                                <asp:TextBox ID="cstb1" runat="server" Height="43px" TextMode="MultiLine" Width="643px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Table Name</td>
                            <td>
                                <asp:TextBox ID="tnametb1" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="bt1" runat="server" OnClick="bt1_Click" Text="Submit" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>

</body>
</html>

 
