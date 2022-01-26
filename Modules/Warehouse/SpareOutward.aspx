<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="SpareOutward.aspx.cs" Inherits="Modules_Warehouse_SpareOutward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; font-weight: bold;">&nbsp;
                            <asp:LinkButton ID="lnkPnl1" runat="server" OnClick="lnkPnl1_Click" Font-Underline="True" CausesValidation="False">Spare Outward</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkPnl2" runat="server" OnClick="lnkPnl2_Click" Font-Underline="True" CausesValidation="False">Spare Outward Search</asp:LinkButton>

                    </td>
                </tr>
            </table>

            <asp:Panel runat="server" ID="pnl1" Visible="true">
                <div id="divSparesInward" style="width: 100%" class="pagehead">
                    <table>
                        <tr>
                            <td style="text-align: left;">Spares Outward
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: right">Spare DC No :</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDCNo" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">Customer Name :
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCustomerName" ErrorMessage="Select any Customer" Font-Bold="True" ForeColor="Red" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                            <td style="text-align: right">Unit Address : 
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtUnitAddress" TextMode="MultiLine" runat="server" Height="60px" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: center">
                                <asp:Button ID="btnSaveWarehouse" runat="server" Text="Sent Outward" OnClick="btnSaveWarehouse_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div id="divSparesOut" style="width: 100%">
                    <asp:GridView ID="gvSpares" runat="server" AllowSorting="True" EmptyDataText="No Records To Display" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvSpares_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Item_Id" HeaderText="Item ID" SortExpression="Item_Id" />
                            <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" SortExpression="Invoice_No" />
                            <asp:BoundField DataField="Model_No" HeaderText="Model No" SortExpression="Model_No" />
                            <asp:BoundField DataField="Spare_Model_No" HeaderText="Spare Model No" SortExpression="Spare_Model_No" />
                            <asp:BoundField DataField="Subcategory" HeaderText="Sub Category" SortExpression="Subcategory" />
                            <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                            <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                            <asp:BoundField DataField="wh_id" HeaderText="Location" SortExpression="whLocId" />
                            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BalanceQty" HeaderText="Qty" SortExpression="BalanceQty" />

                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnl2" Visible="false">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" class="profilehead" style="text-align: left">Spare Outward Details</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Customer Name :</td>
                        <td>
                            <asp:TextBox ID="txtCustName" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">DC NO :</td>
                        <td>
                            <asp:TextBox ID="txtSpDCNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Model No:</td>
                        <td>
                            <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox></td>
                        <td style="text-align: right">Spare Model No :</td>
                        <td>
                            <asp:TextBox ID="txtSpareModelNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">From Date :</td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" type="date"></asp:TextBox></td>
                        <td style="text-align: right">To Date :</td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" type="date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CausesValidation="False" /></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvSpareDtls" runat="server" AllowSorting="True" EmptyDataText="No Records To Display" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="SP_DC_NO" HeaderText="DC NO" SortExpression="SP_DC_NO" />
                                    <asp:BoundField DataField="dt_Added" HeaderText="Date" SortExpression="dt_Added" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Cust_Name" HeaderText="Customer Name" SortExpression="Cust_Name" />
                                    <asp:BoundField DataField="Invoice_No" HeaderText="Invoice No" SortExpression="Invoice_No" />
                                    <asp:BoundField DataField="Model_No" HeaderText="Model No" SortExpression="Model_No" />
                                    <asp:BoundField DataField="Spare_Model_No" HeaderText="Spare Model No" SortExpression="Spare_Model_No" />
                                    <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                                    <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
        
</asp:Content>


 
