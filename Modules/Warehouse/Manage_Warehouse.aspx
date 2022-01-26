<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Manage_Warehouse.aspx.cs" Inherits="Modules_Warehouse_Manage_Warehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td><span class="subh1">Add Warehouse Address</span></td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>Location:</td>
                        <td>
                            <asp:DropDownList ID="ddlLocations1" runat="server" AppendDataBoundItems="True" DataSourceID="locationssds1" DataTextField="locname" DataValueField="locid">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locationssds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM [location_tbl] ORDER BY [locname]"></asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Warehouse Name:</td>
                        <td>
                            <asp:TextBox ID="tbxWarehousename1" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Description:</td>
                        <td>
                            <asp:TextBox ID="tbxWarehouseDesc1" runat="server" Height="100px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Address:</td>
                        <td>
                            <asp:TextBox ID="tbxWarehouseAddr1" runat="server" Height="100px" TextMode="MultiLine" Width="350px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnAddWarehouse1" runat="server" OnClick="btnAddWarehouse1_Click" Text="Add Warehouse" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><span class="subh1">Manage Warehouse Address</span></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="wh_id" DataSourceID="warehousesds1" Width="100%">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="wh_id" HeaderText="wh_id" ReadOnly="True" SortExpression="wh_id" />
                        <asp:BoundField DataField="whname" HeaderText="whname" SortExpression="whname" />
                        <asp:BoundField DataField="whaddr" HeaderText="whaddr" SortExpression="whaddr" />
                        <asp:BoundField DataField="whdesc" HeaderText="whdesc" SortExpression="whdesc" />
                        <asp:BoundField DataField="locid" HeaderText="locid" SortExpression="locid" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="warehousesds1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [warehouse_tbl] WHERE [wh_id] = @original_wh_id AND (([whname] = @original_whname) OR ([whname] IS NULL AND @original_whname IS NULL)) AND (([whaddr] = @original_whaddr) OR ([whaddr] IS NULL AND @original_whaddr IS NULL)) AND (([whdesc] = @original_whdesc) OR ([whdesc] IS NULL AND @original_whdesc IS NULL)) AND (([locid] = @original_locid) OR ([locid] IS NULL AND @original_locid IS NULL))" InsertCommand="INSERT INTO [warehouse_tbl] ([wh_id], [whname], [whaddr], [whdesc], [locid]) VALUES (@wh_id, @whname, @whaddr, @whdesc, @locid)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [warehouse_tbl] ORDER BY [whname]" UpdateCommand="UPDATE [warehouse_tbl] SET [whname] = @whname, [whaddr] = @whaddr, [whdesc] = @whdesc, [locid] = @locid WHERE [wh_id] = @original_wh_id AND (([whname] = @original_whname) OR ([whname] IS NULL AND @original_whname IS NULL)) AND (([whaddr] = @original_whaddr) OR ([whaddr] IS NULL AND @original_whaddr IS NULL)) AND (([whdesc] = @original_whdesc) OR ([whdesc] IS NULL AND @original_whdesc IS NULL)) AND (([locid] = @original_locid) OR ([locid] IS NULL AND @original_locid IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_wh_id" Type="String" />
                        <asp:Parameter Name="original_whname" Type="String" />
                        <asp:Parameter Name="original_whaddr" Type="String" />
                        <asp:Parameter Name="original_whdesc" Type="String" />
                        <asp:Parameter Name="original_locid" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="wh_id" Type="String" />
                        <asp:Parameter Name="whname" Type="String" />
                        <asp:Parameter Name="whaddr" Type="String" />
                        <asp:Parameter Name="whdesc" Type="String" />
                        <asp:Parameter Name="locid" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="whname" Type="String" />
                        <asp:Parameter Name="whaddr" Type="String" />
                        <asp:Parameter Name="whdesc" Type="String" />
                        <asp:Parameter Name="locid" Type="String" />
                        <asp:Parameter Name="original_wh_id" Type="String" />
                        <asp:Parameter Name="original_whname" Type="String" />
                        <asp:Parameter Name="original_whaddr" Type="String" />
                        <asp:Parameter Name="original_whdesc" Type="String" />
                        <asp:Parameter Name="original_locid" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
