<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Stock_ReportNew.aspx.cs" Inherits="Modules_Warehouse_Stock_ReportNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <div style="width: 100%" id="top" runat="server">
        <table style="width: 100%">
            <tr>
                <td class="profilehead">Stock Report
                </td>
            </tr>

        </table>
    </div>

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
                <td style="text-align: right"><%--Executive :--%>
                    <asp:DropDownList ID="ddlExecutive" runat="server" AutoPostBack="True" Visible ="false"></asp:DropDownList>

                </td>
                <td style="width: 5%"></td>
                <%--<td style="text-align: right">Sub Category :
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
                </td>--%>
            </tr>
            <tr>
                <td colspan="4">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="btnSearch" runat="server" Width="10%" Text="Search" BackColor="#CBC9AB" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnRefresh" runat="server" Width="10%"  Text="Refresh" />
                    <%--<asp:Button ID="btnExprot" runat="server" Text="Export To Excel" OnClick="btnExprot_Click" BackColor="#CBC9AB" EnableTheming="True" Width="10%" />--%>
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
                    <asp:GridView ID="gvWarehouseReport" runat="server" EmptyDataText="No Records To Display" Width="100%" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="InwardStock" HeaderText="Inward Stock" SortExpression="InwardStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="BlockedStock" HeaderText="Blocked Stock" SortExpression="BlockedStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="OutWardStock" HeaderText="Outward Stock" SortExpression="OutWardStock">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TOTAL_AVALIABLE_STOCK" HeaderText="Available Stock" SortExpression="TOTAL_AVALIABLE_STOCK">
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Closing Stock">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnBlockStock" OnClick="lbtnBlockStock_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("ClosingStock1") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>&nbsp; 
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField ="wh_id" Visible ="false"  />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>

    </div>
    <br />
    <table style="width: 100%" id="tblBlockedItems" runat="server" visible="false">
        <tr>
            <td class="profilehead">Stock Items
            </td>
        </tr>
        <tr>
            <td>
                 <asp:GridView ID="gvStockReport" runat="server" Width="100%" OnRowDataBound="gvStockReport_RowDataBound" GridLines="None" AllowPaging="true" PageSize="25" OnPageIndexChanging="gvStockReport_PageIndexChanging" Visible="False" AutoGenerateColumns ="false"  >
                                <Columns >
                                    <asp:BoundField DataField ="Item Code" HeaderText ="Item Code" />
                                    <asp:BoundField DataField ="Model No" HeaderText ="Model No" />
                                    <asp:BoundField DataField ="Brand" HeaderText ="Brand" />
                                    <asp:BoundField DataField ="ITEM_SPEC" HeaderText ="Description" />
                                    <asp:BoundField DataField ="Color" HeaderText ="Colour" />
                                    <asp:BoundField DataField ="ClosingStock" HeaderText ="ClosingStock" />
                                    <asp:BoundField DataField ="CHK_NO" HeaderText ="MRN No" />
                                    <asp:BoundField DataField ="MRNDt" HeaderText ="MRN Dt" />
                                    <asp:BoundField DataField ="CHK_INVOICE_NO" HeaderText ="Inv No" />
                                    <asp:BoundField DataField ="InvDt" HeaderText ="Inv Dt" />
                                    <asp:BoundField DataField ="Warehouse Location" HeaderText ="Warehouse Location" />
                                    <asp:BoundField DataField ="MRN_NO" HeaderText ="Det Id" />
                                    <asp:BoundField DataField ="COLOUR_ID" HeaderText ="color id" />
                                    <asp:BoundField DataField ="Item_ID" HeaderText ="Item ID" />
                                    <asp:TemplateField HeaderText ="Qty" >
                                        <ItemTemplate >
                                            <asp:TextBox ID ="txtqtyw" runat ="server" Text ="1" ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText ="No.of Copies" >
                                        <ItemTemplate >
                                            <asp:TextBox ID ="txtqty" runat ="server" Text='<%# Bind("ClosingStock") %>'  ></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:Image ID="Image" runat="server" EnableTheming="False" Height="150px" ImageUrl='<%# Eval("Image","~/Content/QRCodes/{0}") %>'
                                                Width="150px" /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField ="InwardDt" HeaderText ="InwardDt" />
                                    <asp:BoundField DataField ="ISPrint" HeaderText ="IS Print" />
                                    <asp:BoundField DataField ="PrintQty" HeaderText ="Print Qty" />

                                    <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cbSelectAll" runat="server" Text="All" OnClick="selectAll(this)" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox_row" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

