<%@ Page Title="" Language="C#" MasterPageFile="~/BATMP1.master" AutoEventWireup="true" CodeFile="waste1.aspx.cs" Inherits="waste1" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%;">
    <tr>
        <td>&nbsp;Welcome To Vline App</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>
<%--            <cc2:Editor ID="Editor1" runat="server" Visible="true" ActiveMode="Design" Content="dfdfsdsgfgdfdggdhdhdhdhdfhdhdhdgh" />--%>
        </td>
    </tr>
</table>
</asp:Content>

