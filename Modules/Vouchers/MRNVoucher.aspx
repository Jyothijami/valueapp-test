<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Vouchers/Voucher.master" AutoEventWireup="true" CodeFile="MRNVoucher.aspx.cs" Inherits="Modules_MRNVoucher" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:80%">
        <tr align="center">
            <td style="font-size: large; font-weight: bold;">
             <b>   MRN   </b>
            </td>
        </tr>
    </table>
    <table>
    <tr>
        <td>
    <table width="80%">
        <tr>
            <td>
                <table style="width: 80%;">
                   <tr>
            <td>&nbsp;</td>
        </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
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
                </table>
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Invoice No.: <br />
                                        <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td>Dated : 
                                       <asp:Label ID="lblCurrentDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Supplier&#39;s's Ref<br />
                                        <asp:TextBox ID="txtSuppliersRef" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        Oter's Reference(s) :
                                        <asp:TextBox ID="txtOtherReference" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td>Buyer's Order No.<br />
                            <asp:TextBox ID="txtBuyerOrderNo" runat="server"></asp:TextBox>

                                    </td>
                                    <td>Dated<br />
                                        <asp:TextBox ID="txtBuyerOrderDate" runat="server"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
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
                        <td align="center" style="width:13%">Sl.no</td>
                        <td align="center" style="width:13%">Description</td>
                        <td align="center" style="width:13%">Quantity</td>
                        <td align="center" style="width:13%">Rate</td>
                        <td align="center" style="width:13%">per</td>
                        <td align="center" style="width:13%">,Amount</td>
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
                                    <td style="width:20%">Company&#39;s TIN/Sales Tax No. : </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="txtCompanyTinSalesTaxNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width:20%">&nbsp;</td>
                                    <td style="width:20%">&nbsp;</td>

                                </tr>
                                <tr>
                                    <td style="width:20%">Buyer&#39;s VAT TIN : </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="txtBuyerVatTin" runat="server">28480156898</asp:TextBox>
                                    </td>
                                    <td style="width:20%">&nbsp;</td>
                                    <td style="width:20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Company&#39;s CST No. : </td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="txtCompanyCSTNo" runat="server"></asp:TextBox>
                                    </td>
                                    <td style="width:20%">&nbsp;</td>
                                    <td style="width:20%">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Buyer&#39;s CST No. :</td>
                                    <td style="width:20%">
                                        <asp:TextBox ID="txtBuyerCstNo" runat="server" Text="28480156898"></asp:TextBox>
                                    </td>
                                    <td style="width:20%">&nbsp;</td>

                                    <td style="width:20%">&nbsp;</td>

                                </tr>
                                <tr>
                                    <td style="width:20%">&nbsp;</td>
                                    <td style="width:20%">&nbsp;</td>
                                    <td style="width:20%">&nbsp;</td>
                                    <td style="width:20%">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                        <td align="right">for A<br />

                            Authorised Signatory
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


 
