<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="PaymentsReceivedNew.aspx.cs" Inherits="Modules_SM_PaymentsReceivedNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
          <%--  <script>
                $(function () {
                    $("[name$='txtPRDate'],[name$='txtCashReceivedOn'],[name$='txtDDChequeDate']").datepicker();
                });
    </script>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
                <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td  style="text-align:left;">
               Payment Details </td>
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
                            <asp:TextBox ID="txtPRDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;
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
                                Text="Spares" GroupName="P" />
                            <asp:RadioButton ID="rbnSample" runat="server" GroupName="P" Text="Sample" AutoPostBack="True" OnCheckedChanged="rbnSample_CheckedChanged" />
                        </td>
                        <td style="height: 21px; text-align: right"></td>
                        <td style="height: 21px; text-align: left"></td>
                    </tr>
                    <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                    </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                        Text="Go" />
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                        SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="text-align: right"></td>
                                <td style="text-align: left; width: 439px;"></td>
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
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPONo" runat="server" Text="Order No" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged">
                                
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="lblPODate" runat="server" Text="Order Date" Width="103px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtPODate" runat="server" ReadOnly="True" CssClass="datetext"  EnableTheming="True"></asp:TextBox>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Credit Approval No" Width="119px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:DropDownList ID="ddlCrApp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCrApp_SelectedIndexChanged">
                                
                            </asp:DropDownList></td>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Cr.Approval Date" Width="103px"></asp:Label></td>
                        <td style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtCrAppDt" runat="server" ReadOnly="True" CssClass="datetext"  EnableTheming="True"></asp:TextBox>&nbsp;
                        </td>
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
                            <asp:GridView ID="gvPreviousPayments" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvPreviousPayments_RowDataBound" Width="100%" ShowFooter="True" OnSelectedIndexChanged="gvPreviousPayments_SelectedIndexChanged">
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
                                <asp:ListItem>RTGS</asp:ListItem>
                                <asp:ListItem>NEFT</asp:ListItem>

                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPaymentMode"
                                ErrorMessage="Please Select the  Payment Mode">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblCashReceivedOn" runat="server" Text="Cash Received On" Width="129px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCashReceivedOn" runat="server" type="datepic" Visible="False"  EnableTheming="False"></asp:TextBox>
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
                            <asp:TextBox ID="txtDDChequeDate" runat="server" type="datepic" Visible="False" CssClass="datetext" EnableTheming="False"></asp:TextBox>
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
                                    <asp:GridView ID="gvDonepo" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvDonepo_RowDataBound" ShowFooter="True" Visible="False">
                                        <FooterStyle BackColor="#1AA8BE" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" CausesValidation="false" runat="server" __designer:wfdid="w5" OnClick="lbtnDelete_Click1">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Slno" HeaderText="Slno"></asp:BoundField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
                                            <asp:BoundField DataField="Currency" HeaderText="Currency"></asp:BoundField>
                                            <asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
                                            <asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
                                            <asp:BoundField DataField="Price" HeaderText="Spl Price"></asp:BoundField>
                                            <asp:BoundField DataField="Specifications" HeaderText="Specifications"></asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:dd/MM/YYYY}" DataField="DeliveryDate" HeaderText="Delivery Date">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                            <asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
                                            <asp:BoundField DataField="ColorId" HeaderText="Color Id"></asp:BoundField>
                                            <asp:BoundField DataField="Sales" HeaderText="Sales"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>


 
