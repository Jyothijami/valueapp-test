<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StatementOfSecurityDepositReceipt .aspx.cs" Inherits="Modules_SM_StatementOfSecurityDepositReceipt_" Title="||ERP:SM:StatementOfSecurityDepositReceipt||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td title="|| ERP: SM :CLAIMFORM ||">
                statement of Receipts OF security deposit</td>
        </tr>
    </table>
    <br />
    <table style="width: 88px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 90px">
                    <tr>
                        <td style="text-align: left;">
                            Statement Receipts&nbsp;</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="width: 69px;">
                                        <asp:Label id="lblSearch" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="68px"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SDBG_RECEIPTS_NO">SDBG Receipts No</asp:ListItem>
                                            <asp:ListItem Value="SDBG_RECEIPTS_DATE">SDBG Receipts Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer </asp:ListItem>
                                            <asp:ListItem Value="SDBG_RECEIPTS_STATEMENT_OF">Stetement Of</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click1" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
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
            <td colspan="4" style="text-align: center; height: 19px;">
                <asp:GridView id="gvDepositReceiptsDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSecurityReceipts" OnRowDataBound="gvDepositReceiptsDetails_RowDataBound"
                    Width="100%">
                    <columns>
<asp:BoundField DataField="SDBG_RECEIPTS_ID" HeaderText="SDReceiptsIDHidden">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="SD Receipts No"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SDBG_RECEIPTS_NO") %>'></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnSDReceiptsNo" onclick="lbtnSDReceiptsNo_Click" runat="server" Text='<%# Bind("SDBG_RECEIPTS_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SDBG_RECEIPTS_DATE" HeaderText="SD Receipt Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SDBG_RECEIPTS_STATEMENT_OF" HeaderText="Statement Of">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                </asp:GridView><asp:SqlDataSource id="sdsSecurityReceipts" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SDBG_RECEIPTS_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 49px;">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" CausesValidation="False" onclick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" CausesValidation="False"
                                Text="Edit" OnClick="btnEdit_Click" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False"
                                Text="Delete" OnClick="btnDelete_Click1" /></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 471px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblReceiptsDetails" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right; width: 193px;">
                        </td>
                        <td style="height: 21px; text-align: left; width: 14px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblReceiptS" runat="server" Text="Receipts No" Width="85px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtReceipts" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px; width: 193px;">
                            <asp:Label id="lblDate" runat="server" Text="Receipts Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px; width: 14px;">
                            <asp:TextBox id="txtDate" runat="server" Width="153px"></asp:TextBox><asp:Image id="imgDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="CeDate" runat="server" Format="DD/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 21px;">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name" Width="117px"></asp:Label></td>
                        <td style="text-align: left; height: 21px;">
                            <asp:DropDownList id="ddlCustName" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 21px; width: 193px;">
                            <asp:Label id="lblOf" runat="server" Text="Statement Of"></asp:Label></td>
                        <td style="text-align: left; height: 21px; width: 14px;">
                            <asp:DropDownList id="ddlStatementOf" runat="server">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Security Deposit</asp:ListItem>
                                <asp:ListItem>Bank Guarantee</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                            <asp:Label id="lblSBNo" runat="server" Text="SD No/BG No"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlSDNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSDNo_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right; width: 193px;">
                            <asp:Label id="lblSDDate" runat="server" Text="SD Date"></asp:Label></td>
                        <td style="height: 21px; text-align: left; width: 14px;">
                            <asp:TextBox id="txtSDDate" runat="server">
                            </asp:TextBox><asp:Image id="Image1" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgTenderDate"
                                TargetControlID="txtTenderDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtTenderDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                            <asp:Label id="lblTenderNo" runat="server" Text="Tender No"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlTenderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTenderNo_SelectedIndexChanged1">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right; width: 193px;">
                            <asp:Label id="lblTenderDate" runat="server" Text="Tender Date"></asp:Label></td>
                        <td style="height: 21px; text-align: left; width: 14px;">
                            <asp:TextBox id="txtTenderDate" runat="server">
                            </asp:TextBox><asp:Image id="imgTenderDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceTenderDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgTenderDate"
                                TargetControlID="txtTenderDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeTenderDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtTenderDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                            <asp:Label id="lblSopNo" runat="server" Text="SOP No"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlSOPNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSOPNo_SelectedIndexChanged1">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right; width: 193px;">
                            <asp:Label id="lblSopDate" runat="server" Text="SOP Date"></asp:Label></td>
                        <td style="height: 21px; text-align: left; width: 14px;">
                            <asp:TextBox id="txtSOPDate" runat="server">
                            </asp:TextBox><asp:Image id="imgSOPDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceSOPDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgSOPDate"
                                TargetControlID="txtSOPDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeSOPDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtSOPDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                            <asp:Label id="lblCustomerName" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox id="txtCustomerName" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 21px; text-align: right; width: 193px;">
                            <asp:Label id="lblUnitName" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="height: 21px; text-align: left; width: 14px;">
                            <asp:TextBox id="txtUnitName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 32px;">
                            <asp:Label id="lblAddress" runat="server" Text="Unit Address"></asp:Label></td>
                        <td colspan="3" style="text-align: left; height: 32px;">
                            <asp:TextBox id="txtUnitAddress" runat="server" TextMode="MultiLine" Width="508px"
                                ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblMode" runat="server" Text="Payment Mode"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:DropDownList id="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged1">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>cheque</asp:ListItem>
                                <asp:ListItem>DD</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 24px; width: 193px;">
                            <asp:Label id="lblDueDate" runat="server" Text="DueDate"></asp:Label></td>
                        <td style="text-align: left; height: 24px; width: 14px;">
                            <asp:TextBox id="txtDueDate" runat="server">
                            </asp:TextBox><asp:Image id="imgDueDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:CalendarExtender ID="ceDueDate" runat="server" Format="dd/MM/yyyy"
                                    PopupButtonID="imgDueDate" TargetControlID="txtDueDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeDueDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDueDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right;">
                            </td>
                        <td style="height: 24px; text-align: left">
                            &nbsp;
                        </td>
                        <td style="height: 24px; text-align: right; width: 193px;">
                            </td>
                        <td style="height: 24px; text-align: left; width: 14px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="4" rowspan="2">
                            <table id="tblCheque" runat="server" border="0" cellpadding="0" cellspacing="0" visible="false">
                                <tr>
                                    <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                                        Cheque Details</td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                    <td style="height: 19px; text-align: right">
                                    </td>
                                    <td style="height: 19px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="lblChequeNo" runat="server" Text="Cheque No" Width="114px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtChequeNo" runat="server"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="ftxteChequeNo" runat="server" TargetControlID="txtChequeNo"
                                            ValidChars=".0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label id="lblChequeDate" runat="server" Text="Cheque Date" Width="123px"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox id="txtChequeDate" runat="server"></asp:TextBox>&nbsp;<asp:Image id="imgChequeDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender ID="ceChequeDate" runat="server"
                                                Format="dd/MM/yyyy" PopupButtonID="imgChequeDate" TargetControlID="txtChequeDate">
                                            </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeChequeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtChequeDate" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 38px; text-align: right">
                                        <asp:Label id="lblDetails" runat="server" Text="Bank Details" Width="101px"></asp:Label></td>
                                    <td colspan="3" style="height: 38px; text-align: left">
                                        <asp:TextBox id="txtDetailsOfBank" runat="server" EnableTheming="False" TextMode="MultiLine" Width="491px"></asp:TextBox></td>
                                </tr>
                            </table>
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="1" rowspan="2" style="height: 19px; text-align: right">
                            <asp:Label id="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="5" rowspan="2" style="height: 19px; text-align: right">
                            <asp:TextBox id="txtRemarks" runat="server" EnableTheming="False" TextMode="MultiLine"
                                Width="555px">
                            </asp:TextBox>
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="1" style="text-align: right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="1" style="text-align: right; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="5" style="text-align: right">
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right; width: 193px;">
                        </td>
                        <td style="height: 21px; text-align: left; width: 14px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right; width: 193px;">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left; width: 14px;">
                            <asp:DropDownList id="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td style="text-align: right; width: 193px;">
                        </td>
                        <td style="text-align: left; width: 14px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px; width: 193px;">
                        </td>
                        <td style="height: 19px; width: 14px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 68px">
                            <br />
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click1" /></td>
                                    <td style="width: 69px">
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click1" /></td>
                                    <td style="width: 4px">
                                        <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click1" /></td>
                                    <td style="width: 3px">
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


 
