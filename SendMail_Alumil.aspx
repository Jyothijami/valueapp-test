<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail_Alumil.aspx.cs" Inherits="SendMail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
    <div>
        <table>
            <tr >
                                <td  style="text-align: right">
                                    <asp:FileUpload ID="fileupload1" runat="server"  />

                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btnRead" runat="server" onclick="btnRead_Click" Text="Send Mail" />

                                </td>
                                <td></td>
                                <td></td>
                            </tr>
        </table>

        <table style="width: 73%;">
        <tr>
            <td>
                To
            </td>
            <td>
                <asp:TextBox ID="txttomail" runat="server" Height="35px" Width="358px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Subject
            </td>
            <td>
                 
                <asp:TextBox ID="txtsub" runat="server" TextMode="MultiLine"  Width ="100%" Height ="100px" ></asp:TextBox>
                 <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtsub" EnableSanitization="false"></asp:HtmlEditorExtender>
           <br />
                 </td>
        </tr>
        <tr>
            <td>
                Message
            </td>
            <td>
                <asp:TextBox ID="txtmsg" runat="server" TextMode="MultiLine"  Width ="100%" Height ="300px" 
                    ></asp:TextBox>
                 <asp:HtmlEditorExtender ID="HtmlEditorExtender2" runat="server" TargetControlID="txtmsg" EnableSanitization="false"></asp:HtmlEditorExtender>

            </td>
        </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
