<%@ Page Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="PaymentsReceived.aspx.cs" 
    Inherits="Modules_SM_PaymentsReceived" Title="|| Value App : SM : Payments Received ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
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
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>

                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="PR_NO">Receipt No</asp:ListItem>
                                            <asp:ListItem Value="PR_DATE">Receipt  Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                            <asp:ListItem Value="PR_PAYMENT_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td>
                                        <asp:TextBox id="txtSearchValueFromDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox><%--<asp:Image id="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchFrom" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgFromDate" targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>--%>
                                    </td>
                                    <td>
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                     <td>
                                        <asp:TextBox id="txtSearchValueToDate" runat="server" type="date" EnableTheming="True" Visible="False"
                                            Width="106px">
                                        </asp:TextBox>
                                         </td>
                                    <td>
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td>
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" onclick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                                </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView id="gvPaymentsReceived" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsPaymentsReceived" OnRowDataBound="gvPaymentsReceived_RowDataBound"
                    Width="100%" AllowSorting="True" SelectedRowStyle-BackColor="#c0c0c0">
                    <columns>
<asp:BoundField DataField="PR_ID" HeaderText="PaymentsReceivedIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Receipt No"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" Text='<%# Bind("PR_NO") %>'></asp:TextBox> 
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnPaymentsReceivedNo" onclick="lbtnPaymentsReceivedNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Bind("PR_NO") %>' CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="PR_DATE" HeaderText="Receipt Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SO_NO" HeaderText="Purchase Order No">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PR_PAYMENT_STATUS" HeaderText="PR Status"></asp:BoundField>
</columns>
                    <emptydatatemplate>
No Records Found!
</emptydatatemplate>
                </asp:GridView><asp:SqlDataSource id="sdsPaymentsReceived" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_PAYMENTS_RECEIVED_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: left; height: 19px;">
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
                            <asp:Button id="btnEdit" runat="server" Visible="false" CausesValidation="False" onclick="btnEdit_Click"
                                Text="Edit" /></td>
                        <td colspan="3">
                            <asp:Button id="btnDelete" runat="server" CausesValidation="False" onclick="btnDelete_Click"
                                Text="Delete" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 607px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblPaymentsReceived" runat="server" visible="false" width="100%">
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
                        <td style="text-align: right">
                <asp:Label id="lblPRNo" runat="server" Text="Receipt No"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox id="txtPRNo" runat="server" ReadOnly="True"></asp:TextBox>
                <asp:Label ID="lblPRIdHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="text-align: right">
                            <asp:Label id="lblPRDate" runat="server" Text="Receipt Date" Width="152px"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox id="txtPRDate" runat="server" type="date"  CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<%--<asp:Image id="imgPRDate" runat="server" ImageUrl="~/Images/Calendar.png"
                    >
                </asp:Image>--%><asp:RequiredFieldValidator ID="rfvPRDate" runat="server" ControlToValidate="txtPRDate"
                    ErrorMessage="Please Select the PRDate">*</asp:RequiredFieldValidator><asp:CustomValidator
                        ID="cvPRDate" runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtPRDate"
                        ErrorMessage="Please Enter the PR Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                        SetFocusOnError="True">*</asp:CustomValidator><%--<cc1:CalendarExtender ID="cePRDate" runat="server" Format="dd/MM/yyyy"
                    PopupButtonID="imgPRDate" TargetControlID="txtPRDate">
                </cc1:CalendarExtender>--%>
                <%--<cc1:MaskedEditExtender ID="meePRDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtPRDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 21px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Payments For" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:RadioButton ID="rbSales" runat="server" AutoPostBack="True" OnCheckedChanged="rbSales_CheckedChanged"
                                Text="Sales" GroupName="P" />
                            <asp:RadioButton ID="rbSpares" runat="server" AutoPostBack="True" OnCheckedChanged="rbSpares_CheckedChanged"
                                Text="Spares" GroupName="P" /></td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="lblPONo" runat="server" Text="Order No" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                <asp:DropDownList id="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="0">-- Select Unit Name --</asp:ListItem>
                </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                <asp:Label id="lblPODate" runat="server" Text="Order Date" Width="103px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox id="txtPODate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="True"></asp:TextBox>&nbsp;<asp:Image id="imgPODate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"
                    >
                </asp:Image>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                <asp:Label id="lblCustomer" runat="server" Text="Customer" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                <asp:DropDownList id="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                <asp:Label id="lblUnitName" runat="server" Text="Unit Name" Width="129px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                <asp:DropDownList id="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="0">--</asp:ListItem>
                    <asp:ListItem Value="0">-- Select Customer --</asp:ListItem>
            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                <asp:Label id="lblUnitAddress" runat="server" Text="Unit Address" Width="105px"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox id="txtUnitAddress" runat="server" TextMode="MultiLine" Width="500px" ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt" Height="36px"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 19px;">
                </td>
            <td colspan="3" style="text-align: left; height: 19px;">
                &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            previous Receipts</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPreviousPayments_RowDataBound" Width="1005px" ShowFooter="True">
                                <footerstyle backcolor="#1AA8BE" font-bold="True" ForeColor="Black" />
                                <Columns>
<asp:BoundField DataField="PR_ID" HeaderText="PRIDHidden"></asp:BoundField>
<asp:BoundField DataField="PR_NO" HeaderText="Receipt No."></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="PR_DATE" HeaderText="Receipt Date"></asp:BoundField>
<asp:BoundField DataField="SO_NO" HeaderText="Purchase Order No"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SO_DATE" HeaderText="Purchase Date"></asp:BoundField>
<asp:BoundField DataField="PR_AMT_RECEIVED" HeaderText="Amount Received"></asp:BoundField>
<asp:BoundField DataField="PR_PAYMODE_TYPE" HeaderText="Pay Mode"></asp:BoundField>
</Columns>
                                <EmptyDataTemplate>
                                    No Records Found<br />
                                
</EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label3" runat="server" Text="Advance Amount"></asp:Label></td>
                        <td style="text-align: left">
    <asp:TextBox id="txtAdvanceAmount" runat="server">
    </asp:TextBox></td>
                        <td style="text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Balance Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox ID="txtBalanceAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblInvoiceAmt" runat="server" Text="Total Amount" Width="113px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtTotalAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">
                <asp:Label id="lblAmtReceived" runat="server" Text="Amount Received" Width="124px"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtAmtReceived" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="ftxteAmtReceived" runat="server" TargetControlID="txtAmtReceived"
                    ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 24px;">
                            <asp:Label id="Label4" runat="server" Text="Total Paid"></asp:Label></td>
            <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtTotalPaid" runat="server" Enabled="False"></asp:TextBox></td>
            <td style="text-align: right; height: 24px;">
            </td>
            <td style="text-align: left; height: 24px;">
            </td>
                    </tr>
                    <tr>
            <td class="profilehead" colspan="4" style="height: 20px; text-align: left">
                Receipt Details</td>
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
                <asp:Label id="lblPaymentMode" runat="server" Text="Payment Mode" Width="108px"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPaymentMode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged">
                    <asp:ListItem>--</asp:ListItem>
                    <asp:ListItem>Cash</asp:ListItem>
                    <asp:ListItem>Cheque</asp:ListItem>
                    <asp:ListItem>DD</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPaymentMode"
                    ErrorMessage="Please Select the  Payment Mode">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">
                <asp:Label id="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="129px" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtCashReceivedOn" runat="server" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgCashReceivedOn" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"
                    ></asp:Image><asp:RequiredFieldValidator ID="rfvCashReceivedOn" runat="server" ControlToValidate="txtCashReceivedOn"
                        ErrorMessage="Please Select the Cash Received Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="cvCashReceivedOn" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtCashReceivedOn" ErrorMessage="Please Enter the Cash Received Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                            SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender ID="ceCashReceivedOn" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgCashReceivedOn" TargetControlID="txtCashReceivedOn">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeCashReceivedOn" runat="server" DisplayMoney="Left"
                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtCashReceivedOn" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblDDChequeNo" runat="server" Text="DD/Cheque No" Width="114px" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDDChequeNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                    ValidChars=".0123456789">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td style="text-align: right">
                <asp:Label id="lblDDChequeDate" runat="server" Text="DD/Cheque Date" Width="116px" Visible="False"></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDDChequeDate" runat="server" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox><asp:Image id="imgDDChequeDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"
                    ></asp:Image><asp:RequiredFieldValidator ID="rfvDDChequeDate" runat="server" ControlToValidate="txtDDChequeDate"
                        ErrorMessage="Please Select the DD/Cheque Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                            ID="cvDDChequeDate" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtDDChequeDate" ErrorMessage="Please Enter the DD/Cheque Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                            SetFocusOnError="True">*</asp:CustomValidator><cc1:CalendarExtender ID="ceDDChequeDate" runat="server" Format="dd/MM/yyyy"
                        PopupButtonID="imgDDChequeDate" TargetControlID="txtDDChequeDate">
                    </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="meeDDChequeDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtDDChequeDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblBankDetails" runat="server" Text="Bank Details" Width="101px" Visible="False"></asp:Label></td>
            <td style="text-align: left" colspan="3"><asp:TextBox id="txtBankDetails" runat="server" TextMode="MultiLine" Width="804px" EnableTheming="False" Visible="False"></asp:TextBox></td>
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
                <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                &nbsp;</td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlApprovedBy" runat="server" Visible="False">
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
            <td style="height: 21px" colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

 
