<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="Modules_Home_Email" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Yantra : E-Mail</title>
     <script type="text/javascript" src="js/wysiwyg.js"></script>
		<script type="text/javascript" src="js/wysiwyg-settings.js"></script>
		
		<script type="text/javascript">
			WYSIWYG.attach('txtBody', small); // small setup
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <br />
        <table width="600">
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="To"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtTo" runat="server" Width="600px" CssClass="textbox" EnableTheming="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label2" runat="server" Text="Cc"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtCc" runat="server" Width="600px" CssClass="textbox" EnableTheming="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="Label3" runat="server" Text="Subject"></asp:Label></td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtSubject" runat="server" Width="600px" CssClass="textbox" EnableTheming="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:TextBox ID="txtBody" runat="server" Height="278px" Width="100%" CssClass="textbox" EnableTheming="False" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /><asp:Button ID="Button2" runat="server" Text="Cancel" />
                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

 
