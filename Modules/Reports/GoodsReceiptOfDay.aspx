<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsReceiptOfDay.aspx.cs" Inherits="Modules_Reports_GoodsReceiptOfDay" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                goods receipt of the day report</td>
        </tr>
    </table>
    <table width="600">
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td class="profilehead" colspan="3" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">
                <asp:Label ID="lblPRDate" runat="server" Text="Invoice Date" Width="117px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox><asp:Image ID="imgDate"
                    runat="server" ImageUrl="~/Images/Calendar.png" /><cc1:CalendarExtender ID="ceDate"
                        runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgDate"
                        TargetControlID="txtDate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditIndentDate" runat="server" DisplayMoney="Left"
                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDate"
                    UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnRunReport" runat="server" OnClick="btnRunReport_Click" Text="Run Report" /></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
