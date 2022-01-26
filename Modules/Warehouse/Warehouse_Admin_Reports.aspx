<%@ Page Title="|| Value Line App : Warehouse||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Warehouse_Admin_Reports.aspx.cs" Inherits="Modules_Warehouse_Warehouse_Admin_Reports" %>

<%@ Register Src="~/Modules/widgets/stockReport.ascx" TagPrefix="uc1" TagName="stockReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div style="width: 100%" id="top" runat="server">
        <table style="width: 100%">
            <tr>
                <td class="profilehead">Stock Report
                </td>
            </tr>

        </table>
    </div>
    <br />
    <div id="data" runat="server" style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right">Model No :
                    <asp:TextBox ID="txtModelNo" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td style="text-align: right">Brand :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlBrand" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Category :
                    <asp:DropDownList ID="ddlCat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td style="width: 5%"></td>
                <td style="text-align: right">Sub Category :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlSubCat" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="btnSearch" runat="server" Width="10%" Text="Search" BackColor="#CBC9AB" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Width="10%"  Text="Refresh" OnClick="btnRefresh_Click" />
                    <asp:Label ID="lblLocId" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 100%" id="grid" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="gvWarehouseReport" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvWarehouseReport_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="locname" HeaderText="Location" SortExpression="locname">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InwardStock" HeaderText="Inward Stock" SortExpression="InwardStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Blocked Stock">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnBlockStock" OnClick="lbtnBlockStock_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("BlockedStock") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>&nbsp; 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="OutWardStock" HeaderText="Outward Stock" SortExpression="OutWardStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL_AVALIABLE_STOCK" HeaderText="Available Stock" SortExpression="TOTAL_AVALIABLE_STOCK">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="locid" HeaderText="LocId" SortExpression="locid">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </div>
    <br />
    <table style="width: 100%" id="tblBlockedItems" runat="server" visible="false">
        <tr>
            <td class="profilehead">Blocked Items
            </td>
        </tr>
        <tr>
            <td>                <br />
                <div id="gridBlocked" runat="server" style="width: 100%">
                    <asp:GridView ID="gvBlockedItems" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ITEM_MODEL_NO" HeaderText="Model N0" SortExpression="ITEM_MODEL_NO">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOUR_NAME" HeaderText="Color" SortExpression="COLOUR_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUST_NAME" HeaderText="Customer Name" SortExpression="CUST_NAME">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Delivery_Date" HeaderText="Delivery Date" SortExpression="Delivery_Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>

                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table style="width: 100%" id="tblCust" runat="server" visible="true">
        <tr>
            <td colspan="4" class="profilehead"> Blocked Items for Customer
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align: right">Customer Name :</td>
            <td>
                <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>
                <asp:Button ID="btnCustSearch" runat="server" Text="Search" OnClick="btnCustSearch_Click" />
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="4">                <br />
                <div id="Div1" runat="server" style="width: 100%">
                    <asp:GridView ID="gvCustItems" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="true">                      

                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div id="barChart" runat="server" style="width: 100%">
        <uc1:stockreport runat="server" Visible="false" id="stockReport" />

    </div>
</asp:Content>


 
