<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Warehouse_Dashboard.aspx.cs" Inherits="dboards_Warehouse_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <table style="width: 100%;" cellpadding="5" cellspacing="5">
        <tr>
            <%--<td style="vertical-align: top; width: 33%;">--%>

            <td>
                <asp:GridView ID="gvStock" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"  PageSize="5" Width="100%" OnRowDataBound="gvStock_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                        <asp:BoundField DataField="Item Code" HeaderText="Item Code" SortExpression="Item Code" />
                        <asp:BoundField DataField="Model No" HeaderText="Model No" SortExpression="Model No" />
                        <asp:BoundField DataField="Series Name" HeaderText="Series Name" SortExpression="Series Name" />
                        <asp:BoundField DataField="Opening Stock" HeaderText="Opening Stock" ReadOnly="True" SortExpression="Opening Stock" />
                        <asp:BoundField DataField="Total Inward Stock" HeaderText="Total Inward Stock" ReadOnly="True" SortExpression="Total Inward Stock" />
                        <asp:BoundField DataField="Total Outward Stock" HeaderText="Total Outward Stock" ReadOnly="True" SortExpression="Total Outward Stock" />
                        <asp:BoundField DataField="Total Block Stock" HeaderText="Total Block Stock" ReadOnly="True" SortExpression="Total Block Stock" />
                        <asp:BoundField DataField="Total Available Stock" HeaderText="Total Available Stock" ReadOnly="True" SortExpression="Total Available Stock" />
                        <asp:BoundField DataField="Closing Stock" HeaderText="Closing Stock" ReadOnly="True" SortExpression="Closing Stock" />
                        <asp:BoundField DataField="Warehouse Location" HeaderText="Warehouse Location" ReadOnly="True" SortExpression="Warehouse Location" />
                        <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                        <asp:BoundField DataField="Company Name" HeaderText="Company Name" SortExpression="Company Name" />
                    </Columns>
                </asp:GridView>
                <%--<asp:SqlDataSource ID="MovingDCsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="USP_Stock_DashBoard" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
            </td>
            <%--<td style="vertical-align: top; width: 33%;">
                &nbsp;</td>
            <td style="vertical-align: top; width: 33%;"></td>--%>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 33%;"></td>
            <td style="vertical-align: top; width: 33%;"></td>
            <td style="vertical-align: top; width: 33%;"></td>
        </tr>
    </table>
</asp:Content>


 
