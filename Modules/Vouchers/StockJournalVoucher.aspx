<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Vouchers/Voucher.master" AutoEventWireup="true" CodeFile="StockJournalVoucher.aspx.cs" Inherits="Modules_StockJournalVoucher" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:80%">
        <tr align="center">
            <td>
                <h3>VALUE LINE HOMESTYLE PVT LTD </h3>
            </td>
           </tr>
        <tr align="center">
             <td style="font-size: large; font-weight: bold;">
                <b>Stock Journal Vocher</b>
            </td>
        </tr>
    </table>
    <table style="width:80%">
        <tr><td>
            <table style="width:80%">
        <tr>
            <td align="left" style="width:35%">
                No. : <asp:TextBox runat="server" ID="txtNo"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td align="right" style="width:35%">
                Dated : <asp:Label ID="lblCurrentDate" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
            </td>
            </tr>
        <tr>
            <td>
                <table style="width: 80%; border-color:black">
                    <tr>
                        <td align="center" style="width:15%">Item Name</td>
                        <td align="center" style="width:15%"> Godown <br />Batch/Lot</td>
                        <td align="center" style="width:15%">Quantity</td>
                        <td align="center" style="width:15%">Rate</td>
                        <td align="center" style="width:15%">Amount</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine"></asp:TextBox></td>
                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine"></asp:TextBox></td>

                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine"></asp:TextBox></td>

                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine"></asp:TextBox></td>

                        <td>
                            <asp:TextBox runat="server" TextMode="MultiLine"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="text-align: right">Total</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td>
            <table style="width:100%">
                <tr>
            <td style="width:25%">
                Checked by : 
            </td>
            <td style="width:25%">
                Verified by : 
            </td>
            <td style="width:25%"> 
                Authorised Signatory : 
            </td>
                </tr>
        </table>
            </td>
        </tr>
        </table>
</asp:Content>


 
