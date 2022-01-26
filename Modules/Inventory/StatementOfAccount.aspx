<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StatementOfAccount.aspx.cs" Inherits="Modules_Inventory_StatementOfAccount" Title="|| YANTRA : CRM : Statement Of Account ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                StateMeNt of Account</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 790px">
        <tr>
            <td colspan="4" rowspan="3">
                <table id="tblSIDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="true" width="100%">
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
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label id="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Enter the Customer Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtPhone" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtMobile" runat="server" ReadOnly="True">
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
                        <td style="text-align: right">
                            <asp:Label id="lblSalesOrderNo" runat="server" Text="Sales Order No" Width="101px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlSalesOrderNo" runat="server" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label id="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                ErrorMessage="Please Enter the Sales Order No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblSalesOrderDate" runat="server" Text="Sales Order Date" Width="114px">
                            </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSalesOrderDate" runat="server" ReadOnly="True">
                            </asp:TextBox><asp:Image id="imgSalesOrderDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False">
                            </asp:Image><cc1:calendarextender id="CeSalesOrderDate" runat="server" enabled="False"
                                format="dd/MM/yyyy" popupbuttonid="imgSalesOrderDate" targetcontrolid="txtSalesOrderDate"> </cc1:calendarextender><cc1:maskededitextender
                                    id="MeeSalesOrderDate" runat="server" displaymoney="Left" enabled="True" mask="99/99/9999"
                                    masktype="Date" targetcontrolid="txtSalesOrderDate" userdateformat="MonthDayYear"></cc1:maskededitextender></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            <asp:RadioButtonList id="rbtnListStatement" runat="server" AutoPostBack="True"                                 RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnListStatement_SelectedIndexChanged" RepeatLayout="Flow" Visible="False">
                                <asp:ListItem>DC</asp:ListItem>
                                <asp:ListItem>SI</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblDelivery" runat="server" Text="Delivery Challan No" Width="127px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlDeviveryNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeviveryNo_SelectedIndexChanged" >
                            </asp:DropDownList>&nbsp;<asp:Label id="Label26" runat="server" EnableTheming="False" Font-Bold="False"
                                Font-Names="Verdana" Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>&nbsp;<asp:RequiredFieldValidator
                                    id="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeviveryNo"
                                    ErrorMessage="Please Select the Delivery Challan No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="Label2" runat="server" Text="Delivery Challan Date" Width="146px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtChallanDate" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:Image id="imgChallanDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                Visible="False"></asp:Image><cc1:calendarextender id="CeChallanDate" runat="server"
                                    enabled="False" format="dd/MM/yyyy" popupbuttonid="imgChallanDate" targetcontrolid="txtChallanDate"> </cc1:calendarextender><cc1:maskededitextender
                                        id="MeeChallanDate" runat="server" displaymoney="Left" enabled="True" mask="99/99/9999"
                                        masktype="Date" targetcontrolid="txtChallanDate" userdateformat="MonthDayYear"></cc1:maskededitextender></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="Label1" runat="server" Text="Sales Return"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlSalesReturn" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesReturn_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            <asp:Label ID="lblOrderedItemsHeading" runat="server" EnableTheming="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvItemDetails" runat="server" AutoGenerateColumns="False" 
                                Width="100%" OnRowDataBound="gvItemDetails_RowDataBound" ShowFooter="True">
                                <columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left"><asp:Label ID="lblDeliveredItemsHeading" runat="server" EnableTheming="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvDeliveryChallanItems_RowDataBound" ShowFooter="True">
                                <columns>
<asp:BoundField DataField="DC No" HeaderText=" No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="SPPrice" HeaderText="Special Price "></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            <asp:Label id="lblSalesReturnItems" runat="server" EnableTheming="False"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:GridView id="gvItmDetails" runat="server" AutoGenerateColumns="False"
                                OnRowDataBound="gvItmDetails_RowDataBound" ShowFooter="True"><columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="Vat" HeaderText="Vat"></asp:BoundField>
<asp:BoundField DataField="CST" HeaderText="CST"></asp:BoundField>
<asp:BoundField DataField="Excise" HeaderText="Excise"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data Exists</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 47px">
                            <table id="tblButtons">
                                <tr>
                                    <td>
                                        <asp:Button id="btnPrint" runat="server" CausesValidation="False" 
                                            Text="Print" OnClick="btnPrint_Click" /></td>
                                </tr>
                            </table>
                            </td>
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
            <td>
            </td>
        </tr>
    </table>
</asp:Content>


 
