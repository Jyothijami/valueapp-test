<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
     CodeFile="AMCPaymentsReceived.aspx.cs" Inherits="Modules_Services_AMCPaymentsReceived" Title="|| Value App:Services:AMCPaymentsReceived||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                AMC
                Payments Received</td>
             <td>
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                   
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            AMC
                            Payments Received</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="69px"></asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged1"
                                           >
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="AMCPR_NO">AMCPR No.</asp:ListItem>
                                            <asp:ListItem Value="AMCPR_DATE">AMCPR Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="AMCI_NO">AMCI NO</asp:ListItem>
                                            <asp:ListItem Value="AMCPR_PAYMENT_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" 
                                            Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged1">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchFrom" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgFromDate" targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView id="gvPaymentsReceived" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsAmcPayment" OnRowDataBound="gvPaymentsReceived_RowDataBound"
                    Width="100%" AllowSorting="True">
                    <columns>
<asp:BoundField DataField="AMCPR_ID" SortExpression="AMCPR_ID" HeaderText="AMC PaymentsReceivedIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="AMCPR No" SortExpression="AMCPR_NO"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("AMCPR_NO") %>'></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnPaymentsReceivedNo" onclick="lbtnPaymentsReceivedNo_Click" runat="server" Text='<%# Bind("AMCPR_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" SortExpression="AMCPR_DATE" DataFormatString="{0:dd/MM/yyyy}" DataField="AMCPR_DATE" HeaderText="AMCPR Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AMCI_NO" SortExpression="AMCI_NO" HeaderText="AMCI No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AMCPR_PAYMENT_STATUS" SortExpression="AMCPR_PAYMENT_STATUS" HeaderText="AMCPR Status"></asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="sdsAmcPayment" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICES_AMC_PAYMENTS_RECEIVED_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False" onclick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                Text="Delete" /></td>
                        <td>
                            </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 759px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblServiceReceived" runat="server" visible="false">
                    <tr>
            <td colspan="5" style="text-align: left" class="profilehead">
                General Details</td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right">
            </td>
                        <td style="height: 21px; text-align: left">
                        </td>
            <td style="height: 21px; text-align: left">
            </td>
            <td style="height: 21px; text-align: right;">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 24px;">
                <asp:Label id="lblAMCPRNo" runat="server" Text="AMC PR No"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                        </td>
            <td style="text-align: left; height: 24px;">
                <asp:TextBox id="txtAMCPRNo" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:Label id="lblAMCPRIdHidden" runat="server" Visible="False"></asp:Label></td>
            <td style="text-align: right; height: 24px;"><asp:Label id="Label4" runat="server" Text="AMC PR Date" Width="103px"></asp:Label></td>
            <td style="text-align: left; height: 24px;"><asp:TextBox id="txtAMCPRDate" runat="server" ReadOnly="True">
            </asp:TextBox><asp:Image id="imgAMCPRDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image><cc1:CalendarExtender ID="ceAMCPRDate" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="imgAMCPRDate" TargetControlID="txtAMCPRDate">
            </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeAMCPRDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtAMCPRDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label id="lblInvoiceNo" runat="server" Text="Invoice No" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: left">
                <asp:DropDownList id="ddlInvoiceNo" runat="server" OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblInvoiceDate" runat="server" Text="Invoice Date" Width="94px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtInvoiceDate" runat="server" ReadOnly="True"></asp:TextBox><asp:Image id="imgInvoiceDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
            </asp:Image><cc1:CalendarExtender ID="ceInvoiceDate" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="imgInvoiceDate" TargetControlID="txtInvoiceDate">
            </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeInvoiceDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtInvoiceDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblCustomer" runat="server" Text="Customer" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label id="lblUnitName" runat="server" Text="Unit Name" Width="129px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList></td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 32px;">
                <asp:Label id="lblUnitAddress" runat="server" Text="Unit Address" Width="105px"></asp:Label></td>
                        <td colspan="1" style="height: 32px; text-align: left">
                        </td>
            <td colspan="3" style="text-align: left; height: 32px;">
                &nbsp;<asp:TextBox id="txtUnitAddress" runat="server" TextMode="MultiLine" Width="500px" ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>
                <asp:TextBox ID="txtAMCORDERNO" runat="server" Visible="False" Width="241px">dnt delete it as it carries amc order id</asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right"><asp:Label id="lblPONo" runat="server" Text="PO No" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged" Visible="False">
                </asp:DropDownList>
                <asp:TextBox ID="txtPONO" runat="server"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label id="lblPODate" runat="server" Text="PO Date" Width="103px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtPODate" runat="server" ReadOnly="True"></asp:TextBox>&nbsp;<asp:Image id="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
                </asp:Image><cc1:CalendarExtender ID="cePODate" runat="server" Format="dd/MM/yyyy"
                PopupButtonID="imgPODate" TargetControlID="txtPODate">
            </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meePODate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtPODate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblService" runat="server" Text="Type Of Service"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                        </td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtType" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label1" runat="server" Text="Model Of Equipment" Visible="False"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtEquipment" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Serial No"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                        </td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtSerialNo" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 24px; text-align: right">
                <asp:Label id="Label3" runat="server" Text="Payment Terms"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                <asp:TextBox id="txtPayment" runat="server">
                </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="5" style="height: 24px; text-align: left">
                            previous payments</td>
                    </tr>
                    <tr>
                        <td style="height: 22px; text-align: center" colspan="5">
                            <asp:GridView id="gvPreviousPayments" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvPreviousPayments_RowDataBound" Width="1005px">
                                <columns>
<asp:BoundField DataField="AMCPR_ID" HeaderText="AMCPRIDHidden"></asp:BoundField>
<asp:BoundField DataField="AMCPR_NO" HeaderText="PR No."></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="AMCPR_DATE" HeaderText="PR Date"></asp:BoundField>
<asp:BoundField DataField="AMCI_NO" HeaderText="AMCI No."></asp:BoundField>
<asp:BoundField DataField="AMCI_DATE" HeaderText="AMCI Date"></asp:BoundField>
<asp:BoundField DataField="AMCPR_AMT_RECEIVED" HeaderText="Amount Received"></asp:BoundField>
<asp:BoundField DataField="AMCPR_PAYMODE_TYPE" HeaderText="Pay Mode"></asp:BoundField>
</columns>
                                <emptydatatemplate>
                                    No Records Found<br />
                                
</emptydatatemplate>
                            </asp:GridView>
                            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblInvoiceAmt" runat="server" Text="Invoice Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:TextBox id="txtInvoiceAmt" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">
                <asp:Label id="lblAmtReceived" runat="server" Text="Amount Received" Width="150px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtAmtReceived" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="ftxteAmtReceived" runat="server" TargetControlID="txtAmtReceived"
                    ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="Label5" runat="server" Text="Balance Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:TextBox id="txtBalanceAmount" runat="server" ReadOnly="True">
                </asp:TextBox></td>
            <td style="text-align: right;">
            </td>
            <td style="text-align: left">
            </td>
                    </tr>
                    <tr>
            <td class="profilehead" colspan="5" style="height: 20px; text-align: left">
                Payment Details</td>
                    </tr>
                    <tr>
            <td style="height: 19px; text-align: right">
            </td>
                        <td style="height: 19px; text-align: left">
                        </td>
            <td style="height: 19px; text-align: left">
            </td>
            <td style="height: 19px; text-align: right;">
            </td>
            <td style="height: 19px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblPaymentMode" runat="server" Text="Payment Mode" Width="108px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>Cash</asp:ListItem>
                    <asp:ListItem>Cheque</asp:ListItem>
                    <asp:ListItem>DD</asp:ListItem>
                </asp:DropDownList></td>
            <td style="text-align: right;">
                </td>
            <td style="text-align: left">
                </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblDDChequeNo" runat="server" Text="DD/Cheque No" Width="114px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:TextBox id="txtDDChequeNo" runat="server" Visible="False"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                    ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right;">
                <asp:Label id="lblDDChequeDate" runat="server" Text="DD/Cheque Date" Width="116px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDDChequeDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="imgDDChequeDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image><cc1:CalendarExtender ID="ceDDChequeDate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgDDChequeDate" TargetControlID="txtDDChequeDate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeDDChequeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtDDChequeDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label id="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="129px"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left;">
                <asp:TextBox id="txtCashReceivedOn" runat="server"></asp:TextBox>&nbsp;<asp:Image id="imgCashReceivedOn" runat="server" ImageUrl="~/Images/Calendar.png"
                    ></asp:Image><cc1:CalendarExtender ID="ceCashReceivedOn" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgCashReceivedOn" TargetControlID="txtCashReceivedOn">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeCashReceivedOn" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtCashReceivedOn" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
            <td style="text-align: right;">
                </td>
            <td style="text-align: left;">
                </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblBankDetails" runat="server" Text="Bank Details" Width="101px" Visible="False"></asp:Label></td>
                        <td colspan="1" style="text-align: left">
                        </td>
            <td style="text-align: left" colspan="3"><asp:TextBox id="txtBankDetails" runat="server" TextMode="MultiLine" Width="491px" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td colspan="5" style="text-align: left" class="profilehead">
                Reference Details</td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right">
            </td>
                        <td style="height: 21px; text-align: left">
                        </td>
            <td style="height: 21px; text-align: left">
            </td>
            <td style="height: 21px; text-align: right;">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPreparedBy" runat="server">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <asp:Label id="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlApprovedBy" runat="server">
                </asp:DropDownList></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                </td>
                        <td style="text-align: left">
                        </td>
            <td style="text-align: left">
                </td>
            <td style="text-align: right;">
                </td>
            <td style="text-align: left">
                </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
            </td>
                        <td>
                        </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
                    </tr>
                    <tr>
            <td colspan="5" style="height: 68px">
                <br />
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
                        <td>
                            </td>
                    </tr>
                </table>
            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
            <td style="height: 21px">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
            </td>
        </tr>
    </table>
</asp:Content>



 
