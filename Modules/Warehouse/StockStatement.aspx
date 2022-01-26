<%@ Page Title="|| Value App : Warehouse : Stock Inward ||" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="StockStatement.aspx.cs" Inherits="Modules_Warehouse_StockStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <div id="divStockStatement" style="width:100%">
        <table style="width:100%">
            <tr  class="pagehead">
                <td style="text-align:left">
                    Stock Statement :
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>

                    <asp:GridView ID="gvStockStatement" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" EmptyDataText="No Records To Display" Width="100%" AllowSorting="True">
                        <Columns>
                            <asp:BoundField HeaderText="S.No" />
                            <asp:BoundField HeaderText="Product Code" />
                            <asp:BoundField HeaderText="Item Name" />
                            <asp:BoundField HeaderText="Brand" />
                            <asp:BoundField HeaderText="Color" />
                            <asp:BoundField HeaderText="Opening Stock" />
                            <asp:BoundField HeaderText="Stock Reserved" />
                            <asp:BoundField HeaderText="Stock Rectified/Dispatched" />
                            <asp:BoundField HeaderText="Closing Stock" />
                            <asp:BoundField HeaderText="Stock Location" />
                            <asp:BoundField HeaderText="Remarks" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_StockStatement" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                </td>
            </tr>
        </table>

    </div>
</asp:Content>


 
