<%@ Page Language="C#" MasterPageFile="~/MPs/FinanceMP1.master" AutoEventWireup="true" 
    CodeFile="StatementOfAccount1.aspx.cs" Inherits="Modules_Inventory_StatementOfAccount1" Title="|| Value App : Finance : Statement Of Account ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" class="pagehead" width="100%">
        <tr>
            <td style="text-align:left">
                Statement of Account</td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="4" rowspan="3">
                </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
    </table>
    <table id="tblMain" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="3">
                <table id="tblCustomer" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: right; width: 191px;">
                            <asp:Label id="lblSearch" runat="server" Text="Search Customer"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtSearchModel" runat="server">
                            </asp:TextBox><asp:Button id="btnSearchModelNo" runat="server" BorderStyle="None"
                                CausesValidation="False" CssClass="gobutton" EnableTheming="False" onclick="btnSearchModelNo_Click"
                                Text="Go" /></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 306px;">
                            <asp:SqlDataSource id="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                <selectparameters>
                    <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                        Type="String"  />
                </selectparameters>
                            </asp:SqlDataSource></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 191px;">
                            <asp:Label id="lblCustomer" runat="server" Text="Customer Name" Width="104px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                            </asp:DropDownList><asp:Label id="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCustomerName"
                                ErrorMessage="Please Enter the Customer Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblRegion" runat="server" Text="Region"></asp:Label></td>
                        <td style="text-align: left; width: 306px;">
                            <asp:TextBox id="txtRegion" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 191px;">
                            <asp:Label id="lblAddress" runat="server" Text="Address"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox id="txtAddress" runat="server" ReadOnly="True" TextMode="MultiLine">
                            </asp:TextBox></td>
                        <td style="text-align: right">
                            <asp:Label id="lblPhone" runat="server" Text="Phone"></asp:Label></td>
                        <td style="text-align: left; width: 306px;">
                            <asp:TextBox id="txtPhone" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 24px; width: 191px;">
                            <asp:Label id="lblEmail" runat="server" Text="Email"></asp:Label></td>
                        <td style="text-align: left; height: 24px;">
                            <asp:TextBox id="txtEmail" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                        <td style="text-align: right; height: 24px;">
                            <asp:Label id="lblMobile" runat="server" Text="Mobile"></asp:Label></td>
                        <td style="text-align: left; height: 24px; width: 306px;">
                            <asp:TextBox id="txtMobile" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 191px;">
                            <asp:Label id="Label4" runat="server" Text="Unit Name"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlUnitName" runat="server" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                        </td>
                        <td style="text-align: left; width: 306px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 20px; text-align: center">
                            <asp:RadioButton id="rdbPO" runat="server" AutoPostBack="True" OnCheckedChanged="rdbPO_CheckedChanged"
                                Text="As Per Po" GroupName="a">
                            </asp:RadioButton><asp:RadioButton id="RadioButton2" runat="server" Text="As Per Sample Dcs" AutoPostBack="True" OnCheckedChanged="RadioButton2_CheckedChanged" GroupName="a">
                            </asp:RadioButton>
                            <asp:Button id="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" ValidationGroup="asds" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                <table id="tblPo" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    <tr>
                        <td style="text-align: left" colspan="4" rowspan="7" class="profilehead">
                            As Per Purchase Order
                            &nbsp;&nbsp;
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
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label id="lblSalesOrderNo" runat="server" Text="Purchase Order No" Width="130px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlSalesOrderNo" runat="server" OnSelectedIndexChanged="ddlSalesOrderNo_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label id="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSalesOrderNo"
                                ErrorMessage="Please Enter the Sales Order No" InitialValue="0">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label id="lblSalesOrderDate" runat="server" Text="Purchase Order Date" Width="140px"></asp:Label></td>
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
                            <asp:Label id="Label3" runat="server" Text="Sales Invoice"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList id="ddlsalesinvoice" runat="server" OnSelectedIndexChanged="ddlsalesinvoice_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td>
                        <td style="text-align: right">
                            </td>
                        <td style="text-align: left">
                            </td>
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
                            </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Purchase Order No</td>
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
<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Price" HeaderText="SpPrice"></asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <asp:Label id="Label31" runat="server" Text="PO Terms & Conditions :"></asp:Label><asp:Label
                                id="lblTerms" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Pending Material</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center"><asp:GridView id="gvPendingMaterial" runat="server" AutoGenerateColumns="False" 
                                Width="100%" OnRowDataBound="gvPendingMaterial_RowDataBound" ShowFooter="True">
                            <columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="BalanceQty" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="Price" HeaderText="SpPrice"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="PoQty"></asp:BoundField>
</columns>
                            <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                        </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Delivery Challan</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <asp:GridView id="gvDeliveryChallanItems" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvDeliveryChallanItems_RowDataBound" ShowFooter="True">
                                <columns>
<asp:BoundField DataField="DC No" HeaderText=" No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Colour" HeaderText="Colour"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="SPPrice" HeaderText="SpecialPrice "></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="DeliveryDate"></asp:BoundField>
<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="POQty" HeaderText="POQty"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Devlivery Challan On Extra</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            <asp:GridView id="gvdeliveryChallanExtra" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvdeliveryChallanExtra_RowDataBound" ShowFooter="True">
                                <columns>
<asp:BoundField DataField="DC No" HeaderText=" No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Colour" HeaderText="Colour"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="SPPrice" HeaderText="Special Price "></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="DeliveryDate" HeaderText="Delivery Date"></asp:BoundField>
<asp:BoundField HeaderText="UnitPrice"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
<asp:BoundField DataField="POQty" HeaderText="POQty"></asp:BoundField>
</columns>
                                <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            </td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; height: 20px;">
                            Sales Invoice</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center"><asp:GridView id="gvSalesInvoice" runat="server" AutoGenerateColumns="False"
                                Width="100%" OnRowDataBound="gvSalesInvoice_RowDataBound" ShowFooter="True">
                            <columns>
<asp:BoundField DataField="DC No" HeaderText=" No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
<asp:BoundField DataField="DcNO" HeaderText="DcNO"></asp:BoundField>
<asp:BoundField DataField="LrNo" HeaderText="LrNo"></asp:BoundField>
<asp:BoundField HeaderText="Amount"></asp:BoundField>
</columns>
                            <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                        </asp:GridView></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left">
                            Sales Return</td>
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
                            </asp:GridView>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td colspan="4" style="text-align: left">
                                        Statement of account Summary</td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right" align="right">
                                        <asp:Label id="Label5" runat="server" Text="Ordered Value"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblOrderedValue" runat="server"></asp:Label></td>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right" align="right">
                                        <asp:Label id="Label6" runat="server" Text="Dispatched Value" Width="111px"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblDCvalue" runat="server"></asp:Label></td>
                                    <td style="width: 100px; text-align: right" align="right">
                                        <asp:Label id="Label7" runat="server" Text="Extra Dispatched Value" Width="147px"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblExtraDc" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right" align="right">
                                        <asp:Label id="Label11" runat="server" Text="Goods Return" Width="111px"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblGoodsreturn" runat="server"></asp:Label></td>
                                    <td style="width: 100px; text-align: right" align="right">
                                        <asp:Label id="Label12" runat="server" Text="Invoiced Amount" Width="111px"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblInvoicedAmt" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        <asp:Label id="Label13" runat="server" Text="Balance Amount" Width="111px"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblBalanceAmount" runat="server"></asp:Label></td>
                                    <td style="width: 100px; text-align: right">
                                        <asp:Label id="Label15" runat="server" Text="Balance Invoice Amount" Width="149px"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblBalanceInovieAmount" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; text-align: right">
                                        <asp:Label id="Label17" runat="server" Text="Calculate Vat" Width="111px" Visible="False"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:TextBox id="txtVat" runat="server" Visible="False"></asp:TextBox>
                                        <asp:Button id="btnvatcal" runat="server" OnClick="btnvatcal_Click" Text="Go" Visible="False" />
                                        </td>
                                    <td style="width: 100px; text-align: left">
                                        <asp:Label id="lblVatresult" runat="server" Visible="False"></asp:Label></td>
                                    <td style="width: 100px; text-align: left">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 21px; text-align: left">
                                        <asp:Label id="Label18" runat="server" Text="PO Terms & Conditions :"></asp:Label><asp:Label
                                id="lbltermsg" runat="server"></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                        </td>
                    </tr>
                </table>
                            <table id="tblwithDc" runat="server" visible="false" width="700">
                                <tr>
                                    <td class="profilehead" colspan="4" style="text-align: left">
                                        DC On Sample &amp; Cash</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="Label8" runat="server" Text="Delivery Challan No" Width="127px"></asp:Label></td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:DropDownList id="ddlDeliveryCallanOnsample" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeliveryCallanOnsample_SelectedIndexChanged" >
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="Label9" runat="server" Text="Sales Invoice"></asp:Label></td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:DropDownList id="ddlSalesInvoiceOnDc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesInvoiceOnDc_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label id="Label10" runat="server" Text="Sales Return"></asp:Label></td>
                                    <td colspan="3" style="text-align: left">
                                        <asp:DropDownList id="ddlSalesReturnOnDc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesReturnOnDc_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left" class="profilehead">
                                        Delivery Challan On Sample</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView id="gvExtraItems" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <columns>
<asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
</columns>
                                            <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                                        </asp:GridView></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left" class="profilehead">
                                        Delivery Challan On Cash</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView id="gvDconCash" runat="server" AutoGenerateColumns="False" Width="100%">
                                            <columns>
<asp:BoundField DataField="DC No" HeaderText="DC No"></asp:BoundField>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
</columns>
                                            <emptydatatemplate>
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                                        </asp:GridView></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left" class="profilehead">
                                        Sales Invoice</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView id="gvsalesinvoiceondc" runat="server" AutoGenerateColumns="False"
                                            OnRowDataBound="gvsalesinvoiceondc_RowDataBound">
                                            <columns>
<asp:CommandField ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True"></asp:CommandField>
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
<SPAN style="COLOR: #ff0033">No Data to Dispaly</SPAN>
</emptydatatemplate>
                                        </asp:GridView></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: left" class="profilehead">
                                        Sales Return</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center">
                                        <asp:GridView id="gvSlaesreturn" runat="server" AutoGenerateColumns="False"
                                Width="100%" ShowFooter="True">
                                            <columns>
<asp:BoundField DataField="ItemCode" HeaderText="Item Code"></asp:BoundField>
<asp:BoundField DataField="ModelNo" HeaderText="Model No"></asp:BoundField>
<asp:BoundField DataField="ItemName" HeaderText="Item Name"></asp:BoundField>
<asp:BoundField DataField="UOM" HeaderText="UOM"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity"></asp:BoundField>
<asp:BoundField DataField="Rate" HeaderText="Rate"></asp:BoundField>
</columns>
                                            <emptydatatemplate>
<SPAN style="COLOR: #ff0000">No Data Exits</SPAN>
</emptydatatemplate>
                                        </asp:GridView></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
    </table>
</asp:Content>


 
