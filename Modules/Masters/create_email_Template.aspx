<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/emailTemplatesMP1.master" AutoEventWireup="true" CodeFile="create_email_Template.aspx.cs" Inherits="Modules_Masters_create_email_Template"  validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script src="/js/textarea-to-yui-richedit-master/js/yui-insert-editor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table cellpadding="3" cellspacing="3" style="width:100%">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="3">
                    <tr>
                        <td>Template Name :</td>
                        <td>
                            <asp:TextBox ID="emailtptb1" runat="server" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="emailtptb1" ErrorMessage="&lt;img src='images/error.png' /&gt;" ToolTip="Please Enter Template Name"></asp:RequiredFieldValidator>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Short Unique Code :</td>
                        <td>
                            <asp:TextBox ID="shortcodetb1" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="shortcodetb1" ErrorMessage="&lt;img src='images/error.png' /&gt;" ToolTip="Please Enter Unique Code"></asp:RequiredFieldValidator>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>Subject :</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="subjtb1" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Template :</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbxemt1" runat="server" Height="400px" TextMode="MultiLine" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="addtemplatebt1" runat="server" OnClick="addtemplatebt1_Click" Text="Add Template" />
            </td>
        </tr>
    </table>
</asp:Content>


 
