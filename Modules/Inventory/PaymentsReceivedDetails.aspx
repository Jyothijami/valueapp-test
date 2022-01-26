<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" CodeFile="PaymentsReceivedDetails.aspx.cs" Inherits="Modules_Inventory_PaymentsReceivedDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<script>
        $(function () {
            $("[name$='txtPRDate'],[name$='txtPODate']").datepicker();
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%-- <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align:left">Payments Received From Sales</td>
            </tr>
        </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" id="tblPaymentsReceived" runat="server" visible="false" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPRNo" runat="server" Text="Receipt No"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtPRNo" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblPRIdHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPRDate" runat="server" Text="Receipt Date" Width="152px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtPRDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="rfvPRDate" runat="server" ControlToValidate="txtPRDate"
                                ErrorMessage="Please Select the PRDate">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="cvPRDate" runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtPRDate"
                                    ErrorMessage="Please Enter the PR Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>
                            
                            <cc1:MaskedEditExtender ID="meePRDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtPRDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 21px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Payments For" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:RadioButton ID="rbSales" runat="server" AutoPostBack="True" OnCheckedChanged="rbSales_CheckedChanged"
                                Text="Sales" GroupName="P" />
                            <asp:RadioButton ID="rbSpares" runat="server" AutoPostBack="True" OnCheckedChanged="rbSpares_CheckedChanged"
                                Text="AMC" GroupName="P" /></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPONo" runat="server" Text="Order No" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Unit Name --</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPODate" runat="server" Text="Order Date" Width="103px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="True"></asp:TextBox>&nbsp;
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblUnitName" runat="server" Text="Unit Name" Width="129px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Customer --</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="105px"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtUnitAddress" runat="server" TextMode="MultiLine" Width="500px" ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt" Height="36px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td colspan="3" style="text-align: left; height: 19px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Previous Receipts</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPreviousPayments_RowDataBound" Width="1005px" ShowFooter="True">
                                <FooterStyle BackColor="#1AA8BE" Font-Bold="True" ForeColor="Black" />
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
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Advance Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAdvanceAmount" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Balance Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBalanceAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceAmt" runat="server" Text="Total Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTotalAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblAmtReceived" runat="server" Text="Amount Received" Width="124px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAmtReceived" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteAmtReceived" runat="server" TargetControlID="txtAmtReceived"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label4" runat="server" Text="Total Paid"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtTotalPaid" runat="server"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;"></td>
                        <td style="text-align: left; height: 24px;"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 20px; text-align: left">Receipt Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
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
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPaymentMode"
                                ErrorMessage="Please Select the  Payment Mode">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="129px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCashReceivedOn" runat="server" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:Image ID="imgCashReceivedOn" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                            <asp:RequiredFieldValidator ID="rfvCashReceivedOn" runat="server" ControlToValidate="txtCashReceivedOn"
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
                            <asp:Label ID="lblDDChequeNo" runat="server" Text="DD/Cheque No" Width="114px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblDDChequeDate" runat="server" Text="DD/Cheque Date" Width="116px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeDate" runat="server" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:Image ID="imgDDChequeDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                            <asp:RequiredFieldValidator ID="rfvDDChequeDate" runat="server" ControlToValidate="txtDDChequeDate"
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
                            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details" Width="101px" Visible="False"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtBankDetails" runat="server" TextMode="MultiLine" Width="804px" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                        <td></td>
                        <td></td>
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
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" />

                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                            Text="Delete" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table id="TABLE2" runat="server" visible="false">
                    <tr>
                        <td style="width: 100px">
                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                Width="100%" OnSelectedIndexChanged="gvItemDetails_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True"></asp:CommandField>
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                    <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                    <asp:BoundField HeaderText="Amount"></asp:BoundField>
                                    <asp:BoundField DataField="DeliveryStatus" NullDisplayText="-" HeaderText="Status"></asp:BoundField>
                                    <asp:BoundField DataField="SODetId" HeaderText="SODetIdHidden"></asp:BoundField>
                                    <asp:BoundField DataField="Price" HeaderText="Price"></asp:BoundField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            &nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" colspan="4"></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="sdsPaymentsReceived" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SM_PAYMENTS_RECEIVED_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>--%>

     <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td  style="text-align:left;">
               Payments Received From Sales</td>
            </tr>
        </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: center; height: 607px;">
                <table border="0" cellpadding="0" cellspacing="0" id="tblPaymentsReceived" runat="server" visible="false" width="100%">
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">General Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPRNo" runat="server" Text="Receipt No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPRNo" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="lblPRIdHidden" runat="server" Visible="False"></asp:Label></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPRDate" runat="server" Text="Receipt Date" Width="152px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPRDate" runat="server" type="date" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="rfvPRDate" runat="server" ControlToValidate="txtPRDate"
                                ErrorMessage="Please Select the PRDate">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="cvPRDate" runat="server" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtPRDate"
                                    ErrorMessage="Please Enter the PR Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 21px; text-align: right">
                            <asp:Label ID="Label2" runat="server" Text="Payments For" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:RadioButton ID="rbSales" runat="server" AutoPostBack="True" OnCheckedChanged="rbSales_CheckedChanged"
                                Text="Sales" GroupName="P" />
                            <asp:RadioButton ID="rbSpares" runat="server" Visible="false" AutoPostBack="True" OnCheckedChanged="rbSpares_CheckedChanged"
                                Text="Spares" GroupName="P" /></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPONo" runat="server" Text="Order No" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Unit Name --</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPODate" runat="server" Text="Order Date" Width="103px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True" CssClass="datetext" EnableTheming="True"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlCustomer" runat="server" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblUnitName" runat="server" Text="Unit Name" Width="129px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Customer --</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="105px"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtUnitAddress" runat="server" TextMode="MultiLine" Width="500px" ReadOnly="True" EnableTheming="False" Font-Names="Verdana" Font-Size="8pt" Height="36px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td colspan="3" style="text-align: left; height: 19px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">Previous Receipts</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPreviousPayments_RowDataBound" Width="100%" ShowFooter="True">
                                <FooterStyle BackColor="#1AA8BE" Font-Bold="True" ForeColor="Black" />
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
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Advance Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAdvanceAmount" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Balance Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtBalanceAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblInvoiceAmt" runat="server" Text="Total Amount" Width="113px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtTotalAmount" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblAmtReceived" runat="server" Text="Amount Received" Width="124px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAmtReceived" runat="server"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxteAmtReceived" runat="server" TargetControlID="txtAmtReceived"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label4" runat="server" Text="Total Paid"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtTotalPaid" runat="server" Enabled="False"></asp:TextBox></td>
                        <td style="text-align: right; height: 24px;"></td>
                        <td style="text-align: left; height: 24px;"></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 20px; text-align: left">Receipt Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
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
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPaymentMode"
                                ErrorMessage="Please Select the  Payment Mode">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="129px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCashReceivedOn" runat="server" type="date" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCashReceivedOn" runat="server" ControlToValidate="txtCashReceivedOn"
                                ErrorMessage="Please Select the Cash Received Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="cvCashReceivedOn" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtCashReceivedOn" ErrorMessage="Please Enter the Cash Received Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>
                         
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblDDChequeNo" runat="server" Text="DD/Cheque No" Width="114px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeNo" runat="server" Visible="False"></asp:TextBox><cc1:FilteredTextBoxExtender ID="ftxteDDChequeNo" runat="server" TargetControlID="txtDDChequeNo"
                                ValidChars=".0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblDDChequeDate" runat="server" Text="DD/Cheque Date" Width="116px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtDDChequeDate" runat="server" type="date" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDDChequeDate" runat="server" ControlToValidate="txtDDChequeDate"
                                ErrorMessage="Please Select the DD/Cheque Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="cvDDChequeDate" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtDDChequeDate" ErrorMessage="Please Enter the DD/Cheque Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>
                
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Details" Width="101px" Visible="False"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtBankDetails" runat="server" TextMode="MultiLine" Width="804px" EnableTheming="False" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right;"></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Visible="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td></td>
                        <td></td>
                        <td></td>
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
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 21px" colspan="4"></td>
        </tr>
        <tr>
            <td colspan="4" style="height: 21px">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="sdsPaymentsReceived" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SM_PAYMENTS_RECEIVED_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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


 
