<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="PaymentDetails.aspx.cs" Inherits="Modules_Inventory_PaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $("[name$='txtAMCPRDate'],[name$='txtInvoiceDate'],[name$='txtPODate'],[name$='txtDDChequeDate'],[name$='txtCashReceivedOn']").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
        <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">AMC Payment Details</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">

        <tr>
            <td colspan="4" style="text-align: center; height: 759px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblServiceReceived" runat="server" visible="false">
                    <tr>
                        <td colspan="5" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right;"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAMCPRNo" runat="server" Text="AMC PR No"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtAMCPRNo" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblAMCPRIdHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label4" runat="server" Text="AMC PR Date" Width="103px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtAMCPRDate" runat="server" ReadOnly="True">
                            </asp:TextBox>
                           
                            <cc1:MaskedEditExtender ID="meeAMCPRDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtAMCPRDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No" Width="119px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlInvoiceNo" runat="server" OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date" Width="94px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="True"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="meeInvoiceDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" Width="119px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblUnitName" runat="server" Text="Unit Name" Width="129px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 32px;">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="105px"></asp:Label></td>
                        <td colspan="1" style="height: 32px; text-align: left"></td>
                        <td colspan="3" style="text-align: left; height: 32px;">&nbsp;<asp:TextBox ID="txtUnitAddress" runat="server" TextMode="MultiLine" Width="500px" ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                            <asp:TextBox ID="txtAMCORDERNO" runat="server" Visible="False" Width="241px">dnt delete it as it carries amc order id</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPONo" runat="server" Text="PO No" Width="119px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPONO" runat="server"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblPODate" runat="server" Text="PO Date" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;
                            
                            <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblService" runat="server" Text="Type Of Service"></asp:Label></td>
                        <td style="height: 24px; text-align: left"></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtType" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label1" runat="server" Text="Model Of Equipment" Visible="False"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtEquipment" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Serial No"></asp:Label></td>
                        <td style="height: 24px; text-align: left"></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtSerialNo" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Payment Terms"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox ID="txtPayment" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5" style="height: 24px; text-align: left">Previous Payments</td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: center" colspan="5">
                            <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvPreviousPayments_RowDataBound" Width="1005px" ShowFooter="True">
                                <Columns>
                                    <asp:BoundField DataField="AMCPR_ID" HeaderText="AMCPRIDHidden"></asp:BoundField>
                                    <asp:BoundField DataField="AMCPR_NO" HeaderText="PR No."></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="AMCPR_DATE" HeaderText="PR Date"></asp:BoundField>
                                    <asp:BoundField DataField="AMCI_NO" HeaderText="AMCI No."></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="AMCI_DATE" HeaderText="AMCI Date"></asp:BoundField>
                                    <asp:BoundField DataField="AMCPR_AMT_RECEIVED" HeaderText="Amount Received"></asp:BoundField>
                                    <asp:BoundField DataField="AMCPR_PAYMODE_TYPE" HeaderText="Pay Mode"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records Found<br />

                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceAmt" runat="server" Text="Invoice Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtInvoiceAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAmtReceived" runat="server" Text="Amount Received" Width="150px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAmtReceived" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteAmtReceived" runat="server" TargetControlID="txtAmtReceived"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Balance Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBalanceAmount" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5" style="height: 20px; text-align: left">Payment Details</td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode" Width="108px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem>DD</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDDChequeNo" runat="server" Text="DD/Cheque No" Width="114px" Visible="False"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeNo" runat="server" Visible="False"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDDChequeDate" runat="server" Text="DD/Cheque Date" Width="116px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeDate" runat="server"></asp:TextBox>&nbsp;
                            
                            <cc1:MaskedEditExtender ID="meeDDChequeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDDChequeDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="129px"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtCashReceivedOn" runat="server"></asp:TextBox>&nbsp;
                            
                            <cc1:MaskedEditExtender ID="meeCashReceivedOn" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtCashReceivedOn" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details" Width="101px" Visible="False"></asp:Label></td>
                        <td colspan="1" style="text-align: left"></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtBankDetails" runat="server" TextMode="MultiLine" Width="491px" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: left" class="profilehead">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right;"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="5" style="height: 68px">
                            <br />
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                            Text="Delete" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px"></td>
            <td style="height: 21px"></td>
            <td style="height: 21px;"></td>
            <td style="height: 21px"></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px"></td>
        </tr>
    </table>

    <asp:SqlDataSource ID="sdsAmcPayment" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SERVICES_AMC_PAYMENTS_RECEIVED_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>


 
