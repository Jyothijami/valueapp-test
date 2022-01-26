<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RunSQL.aspx.cs" Inherits="eloi_RunSQL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
            <tr>
                <td>Upload SQL file:</td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnupload1" runat="server" OnClick="btnupload1_Click" Text="Upload" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnExecute1" runat="server" OnClick="btnExecute1_Click" Text="Execute SQL File" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td>SQL:</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Height="229px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text=".exe" OnClick="Button1_Click" />
					<asp:Button ID="Button2" runat="server" Text=".exe2" OnClick="Button2_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
