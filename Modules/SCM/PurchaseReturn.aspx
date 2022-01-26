<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PurchaseReturn.aspx.cs" Inherits="Modules_SCM_PurchaseReturn" Title="|| YANTRA : Purchasing Management : Purchase Return ||"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                purchase Return</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="750">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left">
                <table id="TABLE2" border="0" cellpadding="0" cellspacing="0" onclick="return TABLE2_onclick()"
                    width="100%">
                    <tr>
                        <td style="text-align: left">
                            Purchase Return&nbsp;</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label20" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="PI_NO">Invoice No</asp:ListItem>
                                            <asp:ListItem Value="PI_DATE">Invoice Date</asp:ListItem>
                                            <asp:ListItem Value="CUST_NAME">Customer</asp:ListItem>
                                            <asp:ListItem Value="CUST_CONTACT_PERSON">Contact Person</asp:ListItem>
                                            <asp:ListItem Value="PI_AMOUNT">Amount</asp:ListItem>
                                            <asp:ListItem Value="PI_STATUS">Status1</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:DropDownList id="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                            EnableTheming="False"
                                            Visible="False" Width="50px">
                                            <asp:ListItem Selected="True">=</asp:ListItem>
                                            <asp:ListItem>&lt;</asp:ListItem>
                                            <asp:ListItem>&gt;</asp:ListItem>
                                            <asp:ListItem>&lt;=</asp:ListItem>
                                            <asp:ListItem>&gt;=</asp:ListItem>
                                            <asp:ListItem>R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3" style="width: 35px; height: 25px">
                                        <asp:Label id="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
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
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:TextBox id="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image id="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                        <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="False" format="dd/MM/yyyy"
                                            popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                        <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                            enabled="False" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                            userdateformat="MonthDayYear"></cc1:maskededitextender>
                                    </td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" /></td>
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
            <td colspan="4" style="text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="height: 19px">
                <asp:GridView id="gvInvoiceDetails" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" DataSourceID="SqlDataSource1" OnRowDataBound="gvInvoiceDetails_RowDataBound">
                    <columns>
<asp:BoundField DataField="PR_ID" HeaderText="PurchaseIDHidden"></asp:BoundField>
<asp:TemplateField HeaderText="Purchase Return No"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("PI_NO") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
&nbsp;<asp:LinkButton id="lbtnPrNo" runat="server" Text='<%# Bind("PR_NO") %>' __designer:wfdid="w1" OnClick="lbtnPrNo_Click"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="PR_DATE" HeaderText="Purchase Return Date"></asp:BoundField>
<asp:BoundField DataField="FPO_NO" HeaderText="Pur. Order No."></asp:BoundField>
<asp:BoundField DataField="SUP_NAME" HeaderText="Supplier"></asp:BoundField>
<asp:BoundField DataField="SUP_CONTACT_PERSON" HeaderText="Contact Person"></asp:BoundField>
<asp:BoundField DataField="PI_GROSS_AMT" HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="EMPPREPAREDBY" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="EMPAPPROVEDBY" HeaderText="Approved By"></asp:BoundField>
</columns>
                </asp:GridView><asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_PURCHASERETURN_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
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
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
            <td rowspan="3" style="text-align: center">
                <table id="tblPIDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false">
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Purchase Return details</td>
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
                            <asp:Label id="Label35" runat="server" Text="Purchase Return No" Width="143px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPurchaseNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label36" runat="server" Text="Puchase Return Date" Width="147px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPurchaseReturnDate" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: left" class="profilehead" colspan="4">
                            General Details</td>
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
                            <asp:Label id="lblSalesOrderNo" runat="server" Text="PO No" Width="54px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPONo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged"
                                Width="147px">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblSalesOrderDate" runat="server" Text="PO Date" Width="64px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPODate" runat="server">
                            </asp:TextBox>&nbsp;<asp:Image id="imgCurrentDayTasksFromDate" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;
                            <cc1:calendarextender id="cePODate" runat="server" enabled="True" format="dd/MM/yyyy"
                                popupbuttonid="imgSalesOrderDate" targetcontrolid="txtPODate"></cc1:calendarextender>
                            <cc1:maskededitextender id="meePODate" runat="server" displaymoney="Left" enabled="True"
                                mask="99/99/9999" masktype="Date" targetcontrolid="txtPODate" userdateformat="MonthDayYear"></cc1:maskededitextender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 7px;">
                            <asp:Label id="lblSupplierName" runat="server" Text="Supplier Name"></asp:Label></td>
                        <td style="text-align: left; height: 7px;">
                            <asp:TextBox id="txtSupplierName" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:DropDownList id="ddlSupplierName" runat="server" Visible="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right; height: 7px;">
                            <asp:Label id="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                        <td style="text-align: left; height: 7px;">
                            <asp:TextBox id="txtContactPerson" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblPhone" runat="server" Text="Phone No."></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPhoneNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblMobileNo" runat="server" Text="Mobile No"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMobileNo" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
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
                        <td colspan="4" style="text-align: right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="2" style="text-align: center">
                            <asp:GridView id="gvItDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItDetails_RowDataBound"
                                OnRowEditing="gvItDetails_RowEditing" Width="100%">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Model Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Recevied Date"></asp:BoundField>
<asp:BoundField DataField="Specifications" HeaderText="Specifications">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="Item Type Id"></asp:BoundField>
</columns>
                                <emptydatatemplate>
&nbsp;
</emptydatatemplate>
                            </asp:GridView></td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Items Details</td>
                    </tr>
                    <tr>
                        <td style="height: 20px; text-align: right">
                        </td>
                        <td style="height: 20px; text-align: left">
                        </td>
                        <td style="height: 20px; text-align: right">
                        </td>
                        <td style="height: 20px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="lblItemCode" runat="server" Text="Model No"></asp:Label>
                        </td>
                        <td style="height: 24px; text-align: left">
                            <asp:DropDownList id="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label id="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemType"
                                ErrorMessage="Please Select the Item Type" InitialValue="0" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label13" runat="server" Text="Model Name"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtModelName" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label30" runat="server" Text="Item Category"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtItemCategory" runat="server">
                            </asp:TextBox></td>
                        <td style="height: 24px; text-align: right">
                            <asp:Label id="Label31" runat="server" Text="ItemSubCategory"></asp:Label></td>
                        <td style="height: 24px; text-align: left">
                            <asp:TextBox id="txtItemSubCategory" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblUOM" runat="server" Text="UOM" Width="50px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtUOM" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label33" runat="server" Text="Color"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtColor" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label34" runat="server" Text="Brand"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtBrand" runat="server">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="Label32" runat="server" Text="Ordered Quantity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtOrderedQuantity" runat="server" Visible="False">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right">
                            <asp:Label id="Label23" runat="server" Text="Item Specification"></asp:Label></td>
                        <td colspan="3" style="height: 21px; text-align: left">
                            <asp:TextBox id="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False"
                                ReadOnly="True" TextMode="MultiLine" Width="81%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            <asp:RadioButton id="rbVAT" runat="server" Checked="True" GroupName="vatcst" Text="VAT">
                            </asp:RadioButton><asp:RadioButton id="rbCST" runat="server" GroupName="vatcst" Text="C.S. Tax"></asp:RadioButton></td>
                        <td style="text-align: right">
                            <asp:Label id="Label24" runat="server" Text="Excise" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtExcise" runat="server" Visible="False">0</asp:TextBox><asp:Label
                                id="Label27" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" Text="%" Visible="False"></asp:Label>
                            <br />
                            <cc1:filteredtextboxextender id="ftxteExcise" runat="server" targetcontrolid="txtExcise"
                                validchars=".0123456789"></cc1:filteredtextboxextender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblCSTax" runat="server" Text="CST"></asp:Label><asp:Label id="lblVAT"
                                runat="server" Text="VAT"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtVAT" runat="server">0</asp:TextBox><asp:TextBox id="txtCST" runat="server">0</asp:TextBox><asp:Label
                                id="Label25" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" Text="%"></asp:Label><asp:Label id="Label17" runat="server" EnableTheming="False"
                                    Font-Bold="False" Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;
                            <cc1:filteredtextboxextender id="ftxteVat" runat="server" targetcontrolid="txtVAT"
                                validchars=".0123456789"></cc1:filteredtextboxextender>
                            <cc1:filteredtextboxextender id="ftxteCST" runat="server" targetcontrolid="txtCST"
                                validchars=".0123456789"></cc1:filteredtextboxextender>
                        </td>
                        <td style="text-align: right">
                            <asp:Label id="lblRate" runat="server" Text="Rate"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRate" runat="server">
                            </asp:TextBox><asp:Label id="Label21" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator5" runat="server" ControlToValidate="txtRate" ErrorMessage="Please Enter the Rate"
                                    ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteRate"
                                        runat="server" targetcontrolid="txtRate" validchars=".0123456789"> </cc1:filteredtextboxextender></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblQuantity" runat="server" Text="Quantity"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtQuantity" runat="server" Width="139px">
                            </asp:TextBox><asp:Label id="Label19" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                    id="RequiredFieldValidator6" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity"
                                    ValidationGroup="ip">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteQuantity"
                                        runat="server" filtertype="Numbers" targetcontrolid="txtQuantity" validchars="."> </cc1:filteredtextboxextender></td>
                        <td style="text-align: right">
                            <asp:Label id="lblAmount" runat="server" Text="Amount" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAmount" runat="server" ReadOnly="True" Visible="False">
                            </asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right">
                            </td>
                        <td style="height: 19px; text-align: left">
                            </td>
                        <td style="height: 19px">
                        </td>
                        <td style="height: 19px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button id="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                CssClass="loginbutton" EnableTheming="False" onclick="btnAdd_Click" Text="Add" /><asp:Button
                                    id="btnItemRefresh" runat="server" BackColor="Transparent" BorderStyle="None"
                                    CssClass="loginbutton" EnableTheming="False" onclick="btnItemRefresh_Click" Text="Refresh" /></td>
                    </tr>
                    <tr>
                        <td style="height: 16px">
                        </td>
                        <td style="height: 16px">
                        </td>
                        <td style="height: 16px">
                        </td>
                        <td style="height: 16px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" rowspan="2" style="text-align: center">
                            <asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvItemDetails_RowDataBound"
                                OnRowDeleting="gvItemDetails_RowDeleting" OnRowEditing="gvItemDetails_RowEditing"
                                Width="100%">
                                <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
<asp:BoundField DataField="ItemCode" HeaderText="ItemCode"></asp:BoundField>
<asp:BoundField DataField="ItemType" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="VAT" HeaderText="VAT"></asp:BoundField>
<asp:BoundField DataField="cst" HeaderText="CST"></asp:BoundField>
<asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden"></asp:BoundField>
</columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4">
                            other charges</td>
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
                            <asp:Label id="Label11" runat="server" Text="Miscelleneous"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMiscelleneous" runat="server">
                            </asp:TextBox><cc1:filteredtextboxextender id="ftxteMiscelleneous" runat="server"
                                targetcontrolid="txtMiscelleneous" validchars=".0123456789"> </cc1:filteredtextboxextender></td>
                        <td style="text-align: right">
                            <asp:Label id="Label9" runat="server" Text="Discount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtDiscount" runat="server">
                            </asp:TextBox><asp:Label id="Label22" runat="server" Text="%"></asp:Label><cc1:filteredtextboxextender
                                id="ftxteDiscount" runat="server" targetcontrolid="txtDiscount" validchars=".0123456789"> </cc1:filteredtextboxextender></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label10" runat="server" Text="Gross Amount"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtGrossAmount" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:HiddenField id="txtGrossTotalAmtHidden" runat="server"></asp:HiddenField>
                        </td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label4" runat="server" Text="Remarks"></asp:Label></td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox id="txtRemarks" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="81%">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label12" runat="server" Text="Packing Charges  " Visible="False"></asp:Label></td>
                        <td colspan="1" style="text-align: left">
                            <asp:TextBox id="txtPackingCharges" runat="server" Visible="False">
                            </asp:TextBox><asp:Label id="Label16" runat="server" Text="Insurance" Visible="False"></asp:Label><asp:TextBox
                                id="txtInsurance" runat="server" Visible="False"></asp:TextBox><cc1:filteredtextboxextender
                                    id="ftxtePackingCharges" runat="server" targetcontrolid="txtPackingCharges" validchars=".0123456789"> </cc1:filteredtextboxextender></td>
                        <td colspan="3" style="text-align: left">
                            <asp:Label id="Label15" runat="server" Text="Transportation Charges" Visible="False"
                                Width="148px"></asp:Label><asp:TextBox id="txtTranportationCharges" runat="server"
                                    Visible="False"></asp:TextBox><cc1:filteredtextboxextender id="ftxteTrasncharges"
                                        runat="server" targetcontrolid="txtTranportationCharges" validchars=".0123456789"> </cc1:filteredtextboxextender></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Reference Details</td>
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
                            <asp:Label id="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            <asp:Label id="lblApprovedBy" runat="server" Text="Approved By"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlApprovedBy" runat="server" Enabled="False">
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
                        <td colspan="4" style="height: 47px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnSave" runat="server" CausesValidation="False" onclick="btnSave_Click"
                                            Text="Save" /></td>
                                    <td>
                                        <asp:Button id="btnApprove" runat="server" CausesValidation="False" onclick="btnApprove_Click"
                                            Text="Approve" /></td>
                                    <td>
                                        <asp:Button id="btnRefresh" runat="server" CausesValidation="False" onclick="btnRefresh_Click"
                                            Text="Refresh" /></td>
                                    <td>
                                        <asp:Button id="btnClose" runat="server" CausesValidation="False" onclick="btnClose_Click"
                                            Text="Close" /></td>
                                    <td>
                                        <asp:Button id="btnPrint" runat="server" CausesValidation="False" onclick="btnPrint_Click"
                                            Text="Print" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td style="text-align: right">
            </td>
            <td>
            </td>
        </tr>
    </table>
    &nbsp;
</asp:Content>


 
