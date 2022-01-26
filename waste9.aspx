<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/PurchaseMP1.master" AutoEventWireup="true" CodeFile="waste9.aspx.cs" Inherits="waste9" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<cc1:MaskedEditExtender runat="server"
    TargetControlID="TextBox1" 
    Mask="9999999999"
    MaskType="Number" 
    InputDirection="LefttoRight"
    ErrorTooltipEnabled="True"/>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
   </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

