<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SparesQuotation.aspx.cs" Inherits="Modules_Services_SparesQuotation" Title="|| YANTRA : Services : Spares Quotation ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                spares quotation</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="4" style="text-align: left" class="searchhead">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Spares Quotation</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                            OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="SPARES_QUOT_NO">Quotation No</asp:ListItem>
                                            <asp:ListItem Value="SPARES_QUOT_DATE">Quotation Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="CUST_EMAIL">EMail</asp:ListItem>
                                            <asp:ListItem Value="SPARES_QUOT_STATUS">Status</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False" Visible="False" Width="50px" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged">
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
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                            PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeSearchFromDate" runat="server" DisplayMoney="Left"
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
                                        <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                            Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="meeSearchToDate" runat="server" DisplayMoney="Left" Enabled="False"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText" UserDateFormat="MonthDayYear">
                                        </cc1:MaskedEditExtender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label><asp:Label
                                ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px; text-align: center">
                <asp:GridView ID="gvQuotationDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsSparesQuotation" OnRowDataBound="gvQuotationDetails_RowDataBound">
                    <Columns>
<asp:BoundField DataField="SPARES_QUOT_ID" HeaderText="QuotationIdHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Quotation No"><EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SPARES_QUOT_NO") %>'></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                                <asp:LinkButton ID="lbtnQuotationNo" runat="server" CausesValidation="False" OnClick="lbtnQuotationNo_Click"
                                    Text='<%# Bind("SPARES_QUOT_NO") %>'></asp:LinkButton>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SPARES_QUOT_DATE" HeaderText="Quotation Date"></asp:BoundField>
<asp:BoundField DataField="CUST_NAME" HeaderText="Customer">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_CONTACT_PERSON" HeaderText="Contact Person">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_EMAIL" HeaderText="Email">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="PREPAREDBY" SortExpression="PREPAREDBY" HeaderText="Prepared By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="APPROVEDBY" SortExpression="APPROVEDBY" HeaderText="Approved By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SPARES_QUOT_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SPARES_QUOT_REVISED_KEY" HeaderText="RevisedKeyHidden"></asp:BoundField>
                        <asp:BoundField DataField="SPARESQUOTNO" HeaderText="Quotation No Revised" />
</Columns>
                    <EmptyDataTemplate>
                        No Data Exist!
                    
</EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView><asp:SqlDataSource id="sdsSparesQuotation" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SERVICE_SPARES_QUOTATION_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table1">
                    <tr>
                        <td>
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
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <table border="0" cellpadding="0" cellspacing="0" id="tblQuotationDetails" runat="server"
                    visible="false">
                    <tr>
                        <td class="profilehead" colspan="4">
                            general details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label1" runat="server" Text="Register No" Width="85px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlEnquiryNo" runat="server" OnSelectedIndexChanged="ddlEnquiryNo_SelectedIndexChanged"
                                AutoPostBack="True" Enabled="False">
                            </asp:DropDownList><asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEnquiryNo"
                                ErrorMessage="Please Select  the Enquiry No." InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" Text="Register Date" Width="96px"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtEnquiryDate" runat="server" CssClass="datetext" EnableTheming="False"
                                ReadOnly="True"></asp:TextBox>&nbsp;<asp:Image ID="imgEnquiryDate" runat="server"
                                    ImageUrl="~/Images/Calendar.png" Visible="False" /><cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceEnquiryDate"
                                        runat="server" PopupButtonID="imgEnquiryDate" TargetControlID="txtEnquiryDate"
                                        Enabled="False">
                                    </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="meeEnquiryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtEnquiryDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCustomer" runat="server" Text="Customer Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtCustName" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label45" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" rowspan="2">
                            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left;" rowspan="2">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="40px" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblCity" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label20" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label21" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px;">
                        </td>
                        <td style="text-align: right; height: 19px;">
                        </td>
                        <td style="text-align: left; height: 19px; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvEnquiryProducts" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvEnquiryProducts_RowDataBound">
                                <Columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Priority" HeaderText="Priority">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" style="text-align: right" id="Td1">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            quotation details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblQuotationNo" runat="server" Text="Quotation No" Width="88px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtQuotationNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblQuotationDate" runat="server" Text="Quotation Date" Width="96px"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtQuotationDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<asp:Image
                                ID="imgQuotationDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="DateCustomValidate"
                                ControlToValidate="txtQuotationDate" ErrorMessage="Please Enter the Quotation Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceQuotationDate" runat="server" PopupButtonID="imgQuotationDate"
                                TargetControlID="txtQuotationDate">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditInstallDate" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtQuotationDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            item details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemCode" runat="server" Text="Item Type" Width="63px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label4" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlItemType"
                                ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblItemName" runat="server" Text="Item Name" Width="68px"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="0">-- Select Item Type --</asp:ListItem>
                            </asp:DropDownList><asp:Label ID="Label25" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlItemName"
                                ErrorMessage="Please Select the Item Name" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblUOM" runat="server" Text="UOM"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtItemUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 231px;">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="613px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblQuantity" runat="server" Text="Quantity" Width="54px"></asp:Label></td>
                        <td style="height: 19px; text-align: left;">
                            <asp:TextBox ID="txtQunatity" runat="server">
                            </asp:TextBox><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQunatity"
                                ErrorMessage="Please Enter the Quantity" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteQuantity" runat="server" FilterType="Numbers" TargetControlID="txtQunatity">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="height: 19px; text-align: right">
                            <asp:Label ID="lblRate" runat="server" Text="Rate" Width="30px"></asp:Label></td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                            <asp:TextBox ID="txtRate" runat="server">
                            </asp:TextBox><asp:Label ID="Label27" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRate"
                                ErrorMessage="Please Enter the Rate" ValidationGroup="qi">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxteRate" runat="server" FilterType="Numbers" TargetControlID="txtRate">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                        <td style="height: 21px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Add" OnClick="btnAdd_Click"
                                ValidationGroup="qi" />
                            <asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" Text="Refresh" OnClick="btnItemRefresh_Click"
                                CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td style="height: 13px; text-align: right">
                        </td>
                        <td style="height: 13px; text-align: left;">
                        </td>
                        <td style="height: 13px; text-align: right">
                        </td>
                        <td style="height: 13px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                                OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemType" HeaderText="Item Type" />
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UOM" HeaderText="UOM">
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Rate" HeaderText="Rate">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amount">
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 19px; text-align: left">
                            terms &amp; conditions</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label5" runat="server" Text="Delivery"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtDelivery" runat="server"></asp:TextBox><asp:Label ID="Label28"
                                runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDelivery" runat="server" ControlToValidate="txtDelivery"
                                ErrorMessage="Please Enter the Delivery">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="Label18" runat="server" Text="Payment Terms"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtPaymentTerms" runat="server"></asp:TextBox><asp:Label ID="Label35"
                                runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPayTerms" runat="server" ControlToValidate="txtPaymentTerms"
                                ErrorMessage="Please Enter the Payment Terms">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label19" runat="server" Text="packing charges"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPackingCharges" runat="server"></asp:TextBox><asp:Label ID="Label29"
                                runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPackingChrgs" runat="server" ControlToValidate="txtPackingCharges"
                                ErrorMessage="Please Enter the Packing Charges">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender
                                    ID="ftxtePackingCharges" runat="server" FilterType="Numbers" TargetControlID="txtPackingCharges">
                                </cc1:FilteredTextBoxExtender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label3" runat="server" Text="Currency Type"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server">
                            </asp:DropDownList><asp:Label ID="Label37" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCurrencyType"
                                ErrorMessage="Please Select Currency Type" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label8" runat="server" Text="C.S. Tax"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox ID="txtCST" runat="server"></asp:TextBox><asp:Label ID="Label30" runat="server"
                                EnableTheming="False" Text="%"></asp:Label>
                            <asp:Label ID="Label41" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvCST" runat="server" ControlToValidate="txtCST"
                                ErrorMessage="Please Enter the CS Tax">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label ID="Label7" runat="server" Text="Excise Duty"></asp:Label></td>
                        <td style="text-align: left; height: 24px; width: 231px;">
                            <asp:TextBox ID="txtExciseDuty" runat="server"></asp:TextBox><asp:Label ID="Label38"
                                runat="server" EnableTheming="False" Text="%"></asp:Label>
                            <asp:Label ID="Label42" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtExciseDuty"
                                ErrorMessage="Please Enter the Excise Duty">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 78px;">
                            <asp:Label ID="Label33" runat="server" Text="VAT"></asp:Label></td>
                        <td style="text-align: left; height: 78px;">
                            <asp:TextBox ID="txtVAT" runat="server">
                            </asp:TextBox><asp:Label ID="Label34" runat="server" EnableTheming="False" Text="%"></asp:Label>
                            <asp:Label ID="Label44" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtVAT"
                                ErrorMessage="Please Enter the VAT">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 78px;"><asp:Label ID="Label46" runat="server" Text="Price"></asp:Label></td>
                        <td style="text-align: left; height: 78px; width: 231px;"><asp:TextBox ID="txtPrice" runat="server">
                        </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label10" runat="server" Text="Guarantee" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtGuarantee" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label9" runat="server" Text="Despatch Mode"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:DropDownList ID="ddlDespatchMode" runat="server">
                            </asp:DropDownList><asp:Label ID="Label39" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvDespatchMode" runat="server" ControlToValidate="ddlDespatchMode"
                                ErrorMessage="Please Select the Despatch Mode" InitialValue="0">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label13" runat="server" Text="Insurance" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtInsurance" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="Label11" runat="server" Text="Transportation Charges"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtTransCharges" runat="server"></asp:TextBox><asp:Label ID="Label40"
                                runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvTransCharges" runat="server" ControlToValidate="txtTransCharges"
                                ErrorMessage="Please Enter the Transportation Charges">*</asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="ftxteTransCharges" runat="server" FilterType="Numbers"
                                TargetControlID="txtTransCharges">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label15" runat="server" Text="Validity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtValidity" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                        </td>
                        <td style="width: 231px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label6" runat="server" Text="Other Details"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtOtherSpecs" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="625px"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">
                            Follow Up Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 30px;">
                            <asp:Label ID="lblResponsiblePerson" runat="server" Text="Responsible Person" Width="121px"></asp:Label></td>
                        <td style="text-align: left; height: 30px;">
                            <asp:DropDownList ID="ddlResponsiblePerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResponsiblePerson_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label ID="Label43" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">
                            </asp:Label>
                            <asp:RequiredFieldValidator ID="rfvResPerson" runat="server" ControlToValidate="ddlResponsiblePerson"
                                ErrorMessage="Please Select the Responsible Person" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right; height: 30px;">
                            <asp:Label ID="lblFollowupEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left; height: 30px; width: 231px;">
                            <asp:TextBox ID="txtFollowupEmail" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            </td>
                        <td style="text-align: left;">
                            &nbsp;</td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label49" runat="server" Text="Phone No" Width="121px"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:TextBox ID="txtFollowupPhoneNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            other details</td>
                    </tr>
                    <tr>
                        <td style="height: 10px; text-align: right">
                        </td>
                        <td style="height: 10px; text-align: left">
                        </td>
                        <td style="height: 10px; text-align: right">
                        </td>
                        <td style="height: 10px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left">
                            <asp:CheckBox id="chkIsExpectedOrder" runat="server" Text="Expected Order">
                            </asp:CheckBox></td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left;">
                        </td>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="height: 19px; text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left; width: 231px;">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCheckedBy" runat="server" Text="Checked By" Visible="False"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlCheckedBy" runat="server" Enabled="False" Visible="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px; width: 231px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 49px">
                            <table id="tblButtons" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button id="btnFollowUp" runat="server" CausesValidation="False" onclick="btnFollowUp_Click"
                                            Text="Follow Up" /></td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRevise" runat="server" Text="Revise" OnClick="btnRevise_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRegret" runat="server" Text="Regret" OnClick="btnRegret_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnSlaesOrder" runat="server" Text="Spares Order" OnClick="btnSalesOrder_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnSend" runat="server" Text="Send E-Mail" OnClick="btnSend_Click"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 49px">
                            <table border="0" cellpadding="0" cellspacing="0" id="tblFollowUp" runat="server"
                    visible="false">
                                <tr>
                        <td class="profilehead" colspan="2">
                            follow up details</td>
                                </tr>
                                <tr>
                        <td style="height: 19px; text-align: right">
                        </td>
                        <td style="width: 574px; height: 19px; text-align: left">
                        </td>
                                </tr>
                                <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label24" runat="server" Text="Name" Width="76px"></asp:Label></td>
                        <td style="width: 574px; height: 24px; text-align: left">
                            <asp:TextBox id="txtFollowUpName" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                                </tr>
                                <tr>
                        <td style="height: 52px; text-align: right">
                            <asp:Label id="Label23" runat="server" Text="Follow Up Description" Width="150px"></asp:Label></td>
                        <td style="width: 574px; height: 52px; text-align: left">
                            <asp:TextBox id="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px">
                            </asp:TextBox></td>
                                </tr>
                                <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label id="lblTechnicalDiscussions" runat="server" Text="Technical Discussions"
                                Width="146px"></asp:Label></td>
                        <td style="width: 574px; height: 22px; text-align: left">
                            <asp:DropDownList id="ddlTechnicalDiscussions" runat="server" AutoPostBack="True">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Open</asp:ListItem>
                                <asp:ListItem>Closed</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label52" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server"
                                    ControlToValidate="ddlTechnicalDiscussions" ErrorMessage="Please Select Technical DiscussionsDate"
                                    InitialValue="--" ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                        <td style="height: 22px; text-align: right">
                            <asp:Label id="lblCommercialNegociations" runat="server" Text="Commercial Negociations"
                                Width="173px"></asp:Label></td>
                        <td style="width: 574px; height: 22px; text-align: left">
                            <asp:DropDownList id="ddlCommercialNegociations" runat="server" AutoPostBack="True">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Open</asp:ListItem>
                                <asp:ListItem>Closed</asp:ListItem>
                            </asp:DropDownList><asp:Label id="Label53" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server"
                                    ControlToValidate="ddlCommercialNegociations" ErrorMessage="Please Select Commerical Negociations"
                                    InitialValue="--" ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCompetatorsExistance" runat="server" Text="Competators Existance"
                                Width="155px"></asp:Label></td>
                        <td style="width: 574px; text-align: left">
                            <asp:DropDownList id="ddlCompetatorsExistance" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCompetatorsExistance_SelectedIndexChanged">
                                <asp:ListItem>--</asp:ListItem>
                                <asp:ListItem>Exist</asp:ListItem>
                                <asp:ListItem>Does Not Exist</asp:ListItem>
                            </asp:DropDownList></td>
                                </tr>
                                <tr>
                        <td id="tdRemarkslbl" runat="server" style="height: 52px; text-align: right" visible="false">
                            <asp:Label id="lblRemarks" runat="server" Text="Remarks" Width="62px"></asp:Label></td>
                        <td id="tdRemarksField" runat="server" style="width: 574px; height: 52px; text-align: left"
                            visible="false">
                            <asp:TextBox id="txtRemarks" runat="server" CssClass="multilinetext" EnableTheming="False"
                                Height="40px" TextMode="MultiLine" Width="560px">
                            </asp:TextBox><asp:Label id="lblRemarksMandatory" runat="server" EnableTheming="False"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator11"
                                    runat="server" ControlToValidate="txtRemarks" ErrorMessage="Please Enter Remarks"
                                    ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                        <td id="tdOrderExpDatelbl" runat="server" style="text-align: right" visible="false">
                            <asp:Label id="lblExpectedFwpDate" runat="server" Text="Order Expected Date" Width="137px">
                            </asp:Label></td>
                        <td id="tdOrderExpDateField" runat="server" style="width: 574px; text-align: left"
                            visible="false">
                            <asp:TextBox id="txtExpectedFwpDate" runat="server" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><asp:Label id="lblExpectedDateMandatory" runat="server" EnableTheming="False"
                                ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator10"
                                    runat="server" ControlToValidate="txtExpectedFwpDate" ErrorMessage="Please Enter Order Expected Date"
                                    ValidationGroup="follow">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                        <td style="height: 21px; text-align: right">
                        </td>
                        <td style="width: 574px; height: 21px">
                        </td>
                                </tr>
                                <tr>
                        <td colspan="2" style="height: 49px">
                            <table id="Table2">
                                <tr>
                                    <td>
                                        <asp:Button id="btnFollowUpSave" runat="server" onclick="btnFollowUpSave_Click" Text="Save"
                                            ValidationGroup="follow" /></td>
                                    <td>
                                        <asp:Button id="btnFollowUpRefresh" runat="server" CausesValidation="False" onclick="btnFollowUpRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button id="btnFollowUpHistory" runat="server" CausesValidation="False" onclick="btnFollowUpHistory_Click"
                                            Text="History" /></td>
                                    <td>
                                        <asp:Button id="btnFollowUpClose" runat="server" CausesValidation="False" onclick="btnFollowUpClose_Click"
                                            Text="Close" /></td>
                                </tr>
                            </table>
                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 9px">
                            <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                style="text-align: center" visible="false" width="100%">
                                <tr>
                                    <td class="profilehead" colspan="3">
                                        follow up history</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; width: 748px; height: 314px;">
                                        <asp:GridView id="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            DataSourceID="sdsFoUp" OnRowDataBound="gvFollowUp_RowDataBound">
                                            <columns>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SFU_DATE" SortExpression="SFU_DATE" HeaderText="Date"></asp:BoundField>
<asp:BoundField DataField="EMP_ID" SortExpression="EMP_ID" HeaderText="Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SFU_DESC" SortExpression="SFU_DESC" HeaderText="Description">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SFU_TECH_DISCUSSIONS" HeaderText="Tech Diss.">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SFU_COMM_NEGOS" HeaderText="Comm. Negos">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SFU_COMP_EXISTANCE" HeaderText="Comp. Existance">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SFU_REMARKS" SortExpression="SFU_REMARKS" HeaderText="Remarks">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="SFU_FWP_EXPEXTED_DATE" SortExpression="SFU_FWP_EXPEXTED_DATE" HeaderText="Expected Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
</columns>
                                            <selectedrowstyle backcolor="LightSteelBlue" />
                                        </asp:GridView>
                            <asp:DropDownList ID="ddlSalesPerson" runat="server">
                            </asp:DropDownList>
                                        <asp:SqlDataSource id="sdsFoUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SELECT [CR_FOLLOWUP_DET_ID], [SFU_DESC], [SFU_DATE], [SFU_TECH_DISCUSSIONS], [SFU_COMM_NEGOS], [SFU_COMP_EXISTANCE], [SFU_REMARKS], [SFU_FWP_EXPEXTED_DATE], [EMP_ID] FROM [YANTRA_SPARES_QUOT_FOLLOW_DET]"></asp:SqlDataSource>
                                        <asp:Label id="lblQuotIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Panel ID="Panel1" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" style="font-size: 8pt; font-family: Verdana;
                                    background-image: url(Images/ConfirmBox2.PNG); background-repeat: repeat;">
                                    <tr>
                                        <td style="text-align: left; height: 126px;" background="../../Images/ConfirmBox1.PNG"
                                            width="55">
                                        </td>
                                        <td style="height: 126px;" background="../../Images/ConfirmBox2.PNG" align="center"
                                            valign="top">
                                            <table>
                                                <tr>
                                                    <td colspan="3" rowspan="1" style="height: 43px; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" rowspan="2" style="text-align: left">
                                                        <asp:Label ID="lblMessage" runat="server">After Approve the Quotation Do You Want To Send Quotation To Customer ?</asp:Label></td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" rowspan="1" style="height: 17px; text-align: left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="text-align: center">
                                                        <asp:Button ID="btnConfirmYes" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                            Height="23px" Text="Yes" Width="80px" EnableTheming="False" OnClick="btnConfirmYes_Click"
                                                            CausesValidation="False" />
                                                        &nbsp;
                                                        <asp:Button ID="btnConfirmNo" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                            Height="23px" Text="No" Width="80px" EnableTheming="False" CausesValidation="False" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="height: 126px" background="../../Images/ConfirmBox3.PNG" width="27">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel1"
                                TargetControlID="btnForApproveHidden" RepositionMode="None">
                            </cc1:ModalPopupExtender><asp:Button ID="btnForApproveHidden" runat="server" Text="for approve hidden" CausesValidation="False" />
                        </td>
                    </tr>
                </table>
                <table id="tblHiddenFields" style="width: 101px">
                    <tr>
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False"
                                Visible="False" /></td>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="Jurisdiction" Visible="False"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtJurisdiction" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Erection/Commissioning" Visible="False"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtErrection" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                                CausesValidation="False" Visible="False" /></td>
                        <td>
                            <asp:Label ID="Label17" runat="server" Text="Inspection" Visible="False"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtInspection" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False"></asp:ValidationSummary>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="qi"
                    ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary><asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="follow"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
