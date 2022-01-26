<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Vouchers/Voucher.master" AutoEventWireup="true" CodeFile="DebitNote.aspx.cs" Inherits="Modules_DebitNote" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:80%">
        <tr align="center">
            <td style="font-size: large; font-weight: bold;">
             <b>   Debit Note  </b>
            </td>
        </tr>
    </table>
    <table>
    <tr>
        <td>
    <table width="80%">
        <tr>
            <td>
                <table style="width: 100%;">
                      <tr>
            <td style="vertical-align: top">
                <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
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
                        <td>&nbsp;</td>
                    </tr>
                                <tr>
                                    <td style="height: 42px";>Party :<br />
                                        A<br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Debit Note No. : 
                                        <asp:TextBox ID="txtDebitNoteNo" runat="server" Text="VLTP\DN\14-15\2"></asp:TextBox>
                                    </td>
                                    <td>Dated : 
                                       <asp:Label ID="lblCurrentDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>&nbsp;</td>
                                    <td>Mode of Payments
                            <asp:DropDownList ID="ddlModeOfPayment" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem>DD</asp:ListItem>
                            </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>Supplier's Ref<br />
                                        <asp:TextBox ID="txtSuppliersRef" runat="server"></asp:TextBox>
                                    </td>
                                    <td>Others Reference(s)
                            <asp:TextBox ID="txtOtherReference" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                                <%--<tr>
                                    <td>Buyer's Order No.<br />
                            <asp:TextBox ID="txtBuyerOrderNo" runat="server"></asp:TextBox>

                                    </td>
                                    <td>Dated<br />
                                        <asp:TextBox ID="txtBuyerOrderDate" runat="server"></asp:TextBox>

                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td>Despatch Document No
                            <asp:TextBox ID="txtDespatchDocumentNo" runat="server"></asp:TextBox>

                                    </td>
                                    <td>Dated
                                        <br />
                                        <asp:TextBox ID="txtDespatchDocumentDate" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Despatched&nbsp; through
                            <asp:TextBox ID="txtDespatchThrough" runat="server"></asp:TextBox>

                                    </td>
                                    <td>Destination
                            <asp:TextBox ID="txtDestination" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style2" colspan="2">Terms of Delivery
                            <asp:TextBox ID="txtTermsofDelivery" runat="server" TextMode="MultiLine"></asp:TextBox>

                                    </td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 80%; border-color:black">
                    <tr>
                        <td align="center" style="width:13%"> Sl.no</td>
                        <td align="center" style="width:13%"> Description</td>
                        <td align="center" style="width:13%">Quantity</td>
                        <td align="center" style="width:13%">Rate</td>
                        <td align="center" style="width:13%">per</td>
                        <td align="center" style="width:13%">Amount</td>
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
        <tr>
            <td>
                <table style="width: 80%;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 80%;">
                                <tr>
                                    <td>Company's VAT TIN : 
                                        <asp:TextBox ID="txtCompanyVatTin" runat="server" Text="28480156898"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Company's CST No. :
                                        <asp:TextBox ID="txtCompanyCstNo" runat="server" Text="28480156898"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">for Value Line Trade Pvt Ltd<br />

                            Authorised Signatory
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


 
