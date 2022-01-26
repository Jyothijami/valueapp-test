<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EMDReceived.aspx.cs" Inherits="Modules_SM_EMDReceived" Title="|| ERP : S&M : EMDS Received ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                EMD Received</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="750">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            EMD Received</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="EMDR_NO">Emdr  No</asp:ListItem>
                                            <asp:ListItem Value="EMDR_DATE">Emdr  Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="ENQ_REFERENCE">Tender No</asp:ListItem>
                                            <asp:ListItem Value="PR_PAYMENT_STATUS">EMDR Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged1">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="False" Format="dd/MM/yyyy"
                                            PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                            Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                            UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 54px;">
                <asp:GridView ID="gvEMDSReceived" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsEMDSReceived" OnRowDataBound="gvEMDSReceived_RowDataBound" Width="100%">
                    <Columns>
<asp:BoundField DataField="EMDR_ID" HeaderText="EMDSReceivedIdHidden">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="EMDR No"><EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EMDR_NO") %>'></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                                <asp:LinkButton ID="lbtnEMDSReceivedNo" OnClick="lbtnEMDSReceivedNo_Click" runat="server"
                                    Text='<%# Bind("EMDR_NO") %>' CausesValidation="False"></asp:LinkButton>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="EMDR_DATE" HeaderText="EMDR Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ENQ_REFERENCE" HeaderText="Tender No">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="EMDR_STATUS" HeaderText="Emdr Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</Columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsEMDSReceived" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_EMDS_RECEIVED_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                            ControlID="lblSearchItemHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType"
                            ControlID="lblSearchTypeHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                            ControlID="lblSearchValueHidden"></asp:ControlParameter>
                        <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom"
                            ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 48px;">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                                Text="New" /></td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
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
            <td colspan="4" style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" id="tblEmdReceived" runat="server"
                    visible="false">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblEMDRNo" runat="server" Text="EMDR No"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtEMDRNo" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblEMRDIDHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="lblEMDRDate" runat="server" Text="EMDR Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtEMDRDate" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblUnitName" runat="server" Text="Unit Name" Width="93px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 31px;">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="105px"></asp:Label></td>
                        <td colspan="3" style="text-align: left; height: 31px;">
                            &nbsp;<asp:TextBox ID="txtUnitAddress" runat="server" TextMode="MultiLine" Width="458px"
                                ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblTenderNo" runat="server" Text="Tender No" Width="119px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlTenderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTenderNo_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblTenderDate" runat="server" Text="Tender Date" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTenderDate" runat="server"></asp:TextBox>&nbsp;<asp:Image ID="imgTenderDate"
                                runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                    ID="ceTenderDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgTenderDate"
                                    TargetControlID="txtTenderDate">
                                </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeTenderDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtTenderDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDetails" runat="server" Text="DD Details"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDD" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblDate" runat="server" Text="DD Date"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDDate" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Image ID="imgDDDate"
                                runat="server" ImageUrl="~/Images/Calendar.png">
                            </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblBank" runat="server" Text="Bank Details"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            &nbsp;<asp:TextBox ID="txtTenderBankDetails" runat="server" TextMode="MultiLine"
                                Width="466px" EnableTheming="False" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            previous payments</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvPreviousPayments_RowDataBound" Width="1005px">
                                <Columns>
                                      <asp:BoundField DataField="EMDR_ID" HeaderText="EMRD IdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="EMDR_NO" HeaderText="EMRD No"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="EMDR_DATE"
                                        HeaderText="EMRD Date"></asp:BoundField>
                                    <asp:BoundField DataField="ENQ_REFERENCE" HeaderText="Tender No"></asp:BoundField>
                                    <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="ENQ_DATE"
                                        HeaderText="Tender Date"></asp:BoundField>
                                    <asp:BoundField DataField="EMDR_AMT_RECEIVED" HeaderText="Amount Received"></asp:BoundField>
                                    <asp:BoundField DataField="EMDR_PAYMODE_TYPE" HeaderText="Pay Mode"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No Records Found<br />
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblEMDCharges" runat="server" Text="EMD Charges" Width="119px"></asp:Label></td>
                        <td style="height: 19px; text-align: left">
                            <asp:TextBox ID="txtEmdCharges" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                            <asp:Label ID="lblBalance" runat="server" Text="Balance Amount"></asp:Label></td>
                        <td style="text-align: left; height: 19px;">
                            <asp:TextBox ID="txtBalanceAmount" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 19px;">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount Received"></asp:Label></td>
                        <td style="text-align: left; height: 19px;">
                            <asp:TextBox ID="txtAmtReceived" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteAmtReceived" runat="server" TargetControlID="txtAmtReceived"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                            Payment Details</td>
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
                            <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode" Width="108px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Cash</asp:ListItem>
                                <asp:ListItem>Cheque</asp:ListItem>
                                <asp:ListItem>DD</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDDChequeNo" runat="server" Text="DD/Cheque No" Width="114px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeNo" runat="server" Visible="False"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblDDChequeDate" runat="server" Text="DD/Cheque Date" Width="123px"
                                Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeDate" runat="server" Visible="False"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgDDChequeDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False">
                            </asp:Image><cc1:CalendarExtender ID="ceDDChequeDate" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgDDChequeDate" TargetControlID="txtDDChequeDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeDDChequeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDDChequeDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="137px"
                                Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCashReceivedOn" runat="server" Visible="False"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgCashReceivedOn" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False">
                            </asp:Image><cc1:CalendarExtender ID="ceCashReceivedOn" runat="server" Format="dd/MM/yyyy"
                                PopupButtonID="imgCashReceivedOn" TargetControlID="txtCashReceivedOn">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeCashReceivedOn" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtCashReceivedOn" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details" Width="101px" Visible="False"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtBankDetails" runat="server" TextMode="MultiLine" Width="491px"
                                EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="491px" EnableTheming="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;">
                            </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                        <td style="text-align: right">
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
                    </tr>
                    <tr>
                        <td colspan="4">
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

 
