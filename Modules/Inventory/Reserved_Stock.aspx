<%@ Page Title="|| Value App : Inventory : Reserve Stock History ||" Language="C#" MasterPageFile="~/MPs/InventoryMP1.master" AutoEventWireup="true"
    CodeFile="Reserved_Stock.aspx.cs" Inherits="Modules_Inventory_Reserved_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="divReservedStock" style="width: 100%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="pagehead">
                    <tr>
                        <td class="pagehead" align="right" style="text-align: left;">Reserve Stock History
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>75</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="divReservedStockGrid" style="width: 100%">

                <table width="100%" style="text-align: right">

                    <tr>
                        <td>&nbsp;

                        </td>
                    </tr>
                    <tr class="searchhead">
                        <td style="text-align: left">Reserved Stock :
                        </td>
                        <td>
                            <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                Text="Search By Item Code :"></asp:Label>
                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" ToolTip="Item Code"></asp:TextBox>
                            <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnBlockHistory" runat="server" Text="Reserved Status" OnClick="btnBlockHistory_Click" />
                            &nbsp;<asp:Button ID="btnItemBlock" runat="server" Text="Reserve New Item" OnClick="btnItemBlock_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Panel ID="pnlHistory" runat="server">
                                <h4>View &amp; Release Reserved Stock</h4>
                                <asp:GridView ID="gvReservedStock" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="Item_Code" HeaderText="Item Code" />
                                        <asp:BoundField DataField="Model_No" HeaderText="Model No" />
                                        <asp:BoundField DataField="PO_Id" HeaderText="PO No" />
                                        <asp:BoundField DataField="Warehouse_Location" HeaderText="Warehouse Location" />
                                        <asp:BoundField DataField="Total_Available_Stock" HeaderText="Total Available Stock" />
                                        <asp:TemplateField HeaderText="Total Reserved Stock">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQty" Text='<%# Bind("Total_Block_Stock") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Day_Block_Stock" HeaderText="Day Block Stock" />

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <br />
                                <asp:Button ID="btnReleaseBlock" runat="server" Visible="false" Text="Release Items" OnClick="btnReleaseBlock_Click" />
                            </asp:Panel>

                            <asp:Panel ID="pnlBlockItem" runat="server" Visible="false">
                                <h4>Reserve New Item</h4>
                                
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: right; ">Customer Name :
                                        </td>
                                        <td style="text-align: left; " >
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td style="text-align: right">Unit Address : 
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtUnitAddress" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right; width: 20%;">Brand :
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:DropDownList ID="ddlBrand" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="text-align: right">Model No : 
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right; width: 20%;">Company :
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                                        </td>

                                        <td style="width: 5%"></td>
                                        <td style="text-align: right">Warehouse Location :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlWarehouseLocation" AppendDataBoundItems="True"  runat="server" AutoPostBack="True"  DataSourceID="locsds1" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlWarehouseLocation_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <%--Warehouse Name :--%>
                                            Color :</td>
                                        <td style="text-align: left; width: 20%;">
                                            <%--<asp:DropDownList ID="ddlWarhouseName" runat="server" AutoPostBack="True"> </asp:DropDownList>--%>

                                            <asp:DropDownList ID="ddlColor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="text-align: right">Quantity :
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtQuantity" runat="server" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            
                                            Comment :</td>
                                        <td colspan="4" style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtComment" TextMode="MultiLine" Width="400px" runat="server"></asp:TextBox>                                           
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td colspan="5">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;" colspan="2">
                                            <b>Total Available Stock :</b>
                                            <asp:Label ID="lblAvailableStock" Font-Bold="true" ForeColor="#ff9933" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="text-align: right">
                                            <b>Total Blocked Stock :</b>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblBlockedStock" Font-Bold="true" ForeColor="#ff9933" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" style="text-align: center">
                                            <asp:Button ID="btnBlock" Visible="false" runat="server" Text="Reserve Items" OnClick="btnBlock_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:SqlDataSource ID="locsds1" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
                                            <asp:Label ID="lblCPID" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


 
