<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Stock_Report.ascx.cs" Inherits="Modules_widgets_Stock_Report" %>

<strong>Warehouse Report :</strong>
<asp:GridView ID="gvStock" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="5" Width="100%" OnRowDataBound="gvStock_RowDataBound" OnPageIndexChanging="gvStock_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Brand" HeaderText="Brand" SortExpression="Brand" />
                        <asp:BoundField DataField="Item Code" HeaderText="Item Code" SortExpression="Item Code" />
                        <asp:BoundField DataField="Model No" HeaderText="Model No" SortExpression="Model No" />
                        <asp:BoundField DataField="Series Name" HeaderText="Series Name" SortExpression="Series Name" />
                        <asp:BoundField DataField="Opening Stock" HeaderText="Opening Stock" ReadOnly="True" SortExpression="Opening Stock" />
                        <asp:BoundField DataField="Day Inward Stock" HeaderText="Day Inward Stock" ReadOnly="True" SortExpression="Day Inward Stock" />
                        <asp:BoundField DataField="Total Inward Stock" HeaderText="Total Inward Stock" ReadOnly="True" SortExpression="Total Inward Stock" />
                        <asp:BoundField DataField="Day Outward Stock" HeaderText="Day Outward Stock" ReadOnly="True" SortExpression="Day Outward Stock" />
                        <asp:BoundField DataField="Total Outward Stock" HeaderText="Total Outward Stock" ReadOnly="True" SortExpression="Total Outward Stock" />
                        <asp:BoundField DataField="Total Block Stock" HeaderText="Total Block Stock" ReadOnly="True" SortExpression="Total Block Stock" />
                        <asp:BoundField DataField="Total Available Stock" HeaderText="Total Available Stock" ReadOnly="True" SortExpression="Total Available Stock" />
                        <asp:BoundField DataField="Closing Stock" HeaderText="Closing Stock" ReadOnly="True" SortExpression="Closing Stock" />
                        <asp:BoundField DataField="Warehouse Location" HeaderText="Warehouse Location" ReadOnly="True" SortExpression="Warehouse Location" />
                        <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                        <asp:BoundField DataField="Company Name" HeaderText="Company Name" SortExpression="Company Name" />
                    </Columns>
                </asp:GridView>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
