<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StatementOfSecurityDeposit.aspx.cs" Inherits="Modules_SM_StatementOfSecurityDeposit" Title="||ERP:SM:Statement Of Security Deposit||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

  
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td title="|| ERP: SM :CLAIMFORM ||" style="height: 24px">
                statement of security deposit</td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="750">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; height: 1px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 14px">
                    <tr>
                        <td style="text-align: left;">
                            Statement OfSecurity</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="lblSearch" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" Width="72px"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SDBG_NO">SDBG NO</asp:ListItem>
                                            <asp:ListItem Value="SDBG_DATE">SDBG DATE</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer </asp:ListItem>
                                            <asp:ListItem Value="SDBG_STATEMENT_OF">Statement Of</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged1">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList>.</td>
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
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
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
            <td colspan="4" style="text-align: center;">
                <asp:GridView id="gvSdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSDDetails" OnRowDataBound="gvSdDetails_RowDataBound" Width="100%">
                    <columns>
<asp:BoundField DataField="SDBG_ID" HeaderText="SDIDHidden">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="SD No"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("SDBG_NO") %>'></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnSDNo" runat="server" Text='<%# Bind("SDBG_NO") %>' CausesValidation="False" OnClick="lbtnSDNo_Click"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SDBG_DATE" HeaderText="SD Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SDBG_STATEMENT_OF" HeaderText="Statement Of">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <emptydatatemplate>
No Data Exist!
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="sdsSDDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SDBG_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 45px;">
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
                                Text="Delete" OnClick="btnDelete_Click" /></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblDepositDetails" runat="server"
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
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblSDNo" runat="server" Text="SD No/ BG No" Width="124px"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtSDNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblDate" runat="server" Text="SD Date"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtDate" runat="server">
                            </asp:TextBox><asp:Image id="imgDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="CeDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDate" TargetControlID="txtDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MeeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblTenderNo" runat="server" Text="Tender No"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlTenderNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTenderNo_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblTenderDate" runat="server" Text="Tender Date"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
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
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblSopNo" runat="server" Text="SOP No"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList id="ddlSOPNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSOPNo_SelectedIndexChanged1">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblSopDate" runat="server" Text="SOP Date"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
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
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblCustomerName" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox id="txtCustomerName" runat="server">
                            </asp:TextBox>
                            <asp:Label id="lblCUSTIdHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblUnitName" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
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
                        <td style="text-align: right; height: 22px;">
                            <asp:Label id="lblOf" runat="server" Text="Statement Of"></asp:Label></td>
                        <td colspan="1" style="text-align: left; height: 22px;">
                            <asp:DropDownList id="ddlStatementOf" runat="server" OnSelectedIndexChanged="ddlStatementOf_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Security Deposit</asp:ListItem>
                                <asp:ListItem>Bank Guarantee</asp:ListItem>
                            </asp:DropDownList></td>
                        <td colspan="1" style="text-align: right; height: 22px;">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name" Visible="False"></asp:Label></td>
                        <td colspan="3" style="text-align: left; height: 22px;">
                            <asp:DropDownList id="ddlCustName" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="4" id="tblSdDetails" rowspan="2"><table border="0" cellpadding="0" cellspacing="0" id="tblsdDetils" runat="server"
                    visible="false">
                            <tr>
                                <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                                    SD /BG &nbsp;Details</td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;">
                                </td>
                                <td style="height: 21px; text-align: left;">
                                </td>
                                <td style="height: 21px; text-align: right;">
                                </td>
                                <td style="height: 21px; text-align: left;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label id="lblBGNo" runat="server" Text="BG No"></asp:Label><asp:Label id="lblSDBgNo" runat="server" Text="SD No"></asp:Label>&nbsp;</td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox id="txtSecurityNo" runat="server">
                                    </asp:TextBox></td>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label id="lblDd" runat="server" Text="DD No"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox id="txtDd" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                    <asp:Label id="lblDDate" runat="server" Text="DD Date"></asp:Label></td>
                                <td style="height: 24px; text-align: left">
                                    <asp:TextBox id="txtDDate" runat="server">
                                    </asp:TextBox><asp:Image id="imgDDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceDDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDDate"
                                TargetControlID="txtDDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="meeDDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDDate" UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td style="height: 24px; text-align: right;">
                                    <asp:Label id="lblAmoun" runat="server" Text="Amount"></asp:Label></td>
                                <td style="height: 24px; text-align: left">
                                    <asp:TextBox id="txtAmt" runat="server">
                                    </asp:TextBox>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                    <asp:Label id="lblBan" runat="server" Text="Bank"></asp:Label></td>
                                <td style="height: 24px; text-align: left" colspan="3">
                                    <asp:TextBox id="txtBankDetails" runat="server" EnableTheming="False" TextMode="MultiLine"
                                        Width="505px">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                    <asp:Label id="lblDuDate" runat="server" Text="Due Date"></asp:Label></td>
                                <td colspan="3" style="height: 24px; text-align: left">
                                    <asp:TextBox id="txtDue" runat="server">
                                    </asp:TextBox><asp:Image id="imgDue" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image><cc1:CalendarExtender
                                ID="ceDue" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgDue"
                                TargetControlID="txtDue">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="meeDue" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtDue" UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                    <asp:Label id="lblRe" runat="server" Text="Remarks"></asp:Label></td>
                                <td colspan="3" style="height: 24px; text-align: left">
                                    <asp:TextBox id="txtRemark" runat="server" TextMode="MultiLine" Width="508px"
                                EnableTheming="False" Font-Names="Verdana"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                </td>
                                <td colspan="3" style="height: 24px; text-align: center">
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                </td>
                                <td colspan="3" style="height: 24px; text-align: center">
                                    <asp:Button id="btnAdding" runat="server" BackColor="Transparent" BorderStyle="None"
                                        CssClass="loginbutton" EnableTheming="False" onclick="btnAdding_Click" Text="Add"
                                        ValidationGroup="cc" /><asp:Button id="btnRefreshItems" runat="server" BackColor="Transparent"
                                            BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                            onclick="btnItemRefresh_Click" Text="Refresh" /></td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvPreviousPayments" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvPreviousPayments_RowDataBound" Width="1005px">
                                <columns>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="SDNumber" HeaderText="SDNumber"></asp:BoundField>
<asp:BoundField DataField="DDNo" HeaderText="DDNo"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataField="DDDate" HeaderText="DD Date"></asp:BoundField>
<asp:BoundField DataField="Amount" HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Bank" HeaderText="Bank"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataField="DueDate" HeaderText="Due Date"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks"></asp:BoundField>
</columns>
                                <emptydatatemplate>
                                    No Records Found<br />
                                
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="2" style="text-align: right">
                        </td>
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
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList id="ddlPreparedBy" runat="server">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By" Width="96px"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:DropDownList id="ddlApprovedBy" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px;">
                        </td>
                        <td style="height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 68px">
                            <br />
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td style="width: 69px">
                                        <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></td>
                                    <td style="width: 4px">
                                        <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" /></td>
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

 
