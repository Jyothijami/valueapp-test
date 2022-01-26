<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Vouchers/Voucher.master" AutoEventWireup="true" CodeFile="BankReceiptVoucher1.aspx.cs" Inherits="Modules_BankReceiptVoucher1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

       <table align="center">
        <tr>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
            </td>
            </tr>
       <tr>
            <td>
                <asp:Label ID="lblAddress" runat="server" Visible="true"></asp:Label>
            </td>
        </tr>
           <tr>
               <td>
                   Email : <asp:Label ID="lblEmail" runat="server"></asp:Label>
               </td>
           </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td style="text-align:center"><b>Bank&nbsp; Receipt Voucher</b></td>
        </tr>
    </table>
    <br />
    <table style="width:100%">
        <tr>
            <td style="width:8%">
                No :
            </td>
            <td style="width:55%">
                <asp:TextBox ID="txtNo" runat="server"></asp:TextBox>
            </td>
            <td style="width:8%">
                Date :
            </td>
            <td>
                <asp:Label ID="lblDate" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
    <table style="width:100%;border-style:groove">
        <tr style="border-style:groove">
            <td colspan="2" style="text-align:center;border-right-style:groove;border-color:#777;">
               <b> Particulars</b>
            </td>
            <td style="text-align:center;">
               <b>  Amount</b>
            </td>
            <td style="width:10%"></td>
        </tr>
        <tr>
            <td>
                Account :
            </td>
            <td>
                <asp:TextBox ID="txtAccount" TextMode="MultiLine" runat="server" Width="300px" Height="160px"></asp:TextBox>
            </td>
             <td colspan="2" style="text-align:center">
                <asp:TextBox ID="txtamount" TextMode="MultiLine" runat="server" Width="300px" Height="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Through :
            </td>
            <td>
                <asp:DropDownList ID="ddlPaymentType" runat="server">
                    <asp:ListItem Value="0">UCO Bank CC_09790500011480</asp:ListItem>
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                    <asp:ListItem Value="3"></asp:ListItem>
                    <asp:ListItem Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                Amount (in Words) :
            </td>
            <td>
                <asp:TextBox ID="txtAmountInWords" TextMode="MultiLine" runat="server" Width="300px" Height="60px"></asp:TextBox>
            </td>
            <td colspan="2" style="text-align:center">Total : 
                <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table style="width:100%">
        <tr>
            <td style="width:50%">&nbsp;</td>
            <td style="width:50%">
                Authorised Signatory :
            </td>
        </tr>
        <tr>
            <td style="width:50%">&nbsp;</td>
            <td style="width:50%">&nbsp;</td>
        </tr>
        <tr>
            <td style="width:50%">
                Checked By :
            </td>
            <td style="width:50%">
                Verified By :
            </td>
        </tr>
    </table>
</asp:Content>


 
