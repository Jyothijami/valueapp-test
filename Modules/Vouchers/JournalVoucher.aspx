<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Vouchers/Voucher.master" AutoEventWireup="true" CodeFile="JournalVoucher.aspx.cs" Inherits="Modules_JournalVoucher" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

       <table align="center">
        <tr>
            <td>
                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
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
            <td style="text-align:center"><b>Journal Voucher</b></td>
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
                <b>  Debit</b>
            </td>
            <td style="width:10%"><b>  Credit</b></td>
        </tr>
         <tr>
            <td>
                To&nbsp;  A
            </td>
            <td>
                <asp:TextBox ID="txtAccount" TextMode="MultiLine" runat="server" Width="300px" Height="160px"></asp:TextBox>
                Dr </td>
             <td colspan="2" style="text-align:center">
                <asp:TextBox ID="txtDebit" TextMode="MultiLine" runat="server" Width="180px" Height="210px"></asp:TextBox>
                <asp:TextBox ID="txtCredit" TextMode="MultiLine" runat="server" Width="180px" Height="210px"></asp:TextBox>
            </td>
        </tr>
        <tr>
       <%--     <td>
                Through :
            </td>
            <td>
                <asp:DropDownList ID="ddlPaymentType" runat="server">
                    <asp:ListItem Value="0">Cash</asp:ListItem>
                    <asp:ListItem Value="1">Credit Card</asp:ListItem>
                    <asp:ListItem Value="2">Debit Card</asp:ListItem>
                    <asp:ListItem Value="3">Net Banking</asp:ListItem>
                    <asp:ListItem Value="4">Cheque</asp:ListItem>
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
            </td>--%>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td colspan="2" style="text-align:center">Total : 
                <asp:TextBox ID="txtTotalDebit" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTotalCredit" runat="server"></asp:TextBox>

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


 
