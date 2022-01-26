<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/WarehouseMP1.master" AutoEventWireup="true" CodeFile="Manage_Locations2.aspx.cs" Inherits="Modules_Warehouse_Manage_Locations2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table cellpadding="5" cellspacing="5" style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Select Location : </td>
                        <td>
                            <asp:DropDownList ID="ddlLocations1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="locsds1" DataTextField="locname" DataValueField="locid" OnSelectedIndexChanged="ddlLocations1_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="locsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Select Branch:</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch1" runat="server" AutoPostBack="True" DataSourceID="branchsds1" DataTextField="whname" DataValueField="wh_id" OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="branchsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [wh_id], [whname] FROM [warehouse_tbl] WHERE ([locid] = @locid)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlLocations1" Name="locid" PropertyName="SelectedValue" Type="String" DefaultValue="1" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="vertical-align: top">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="3" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                                        </asp:TreeView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--                                        <asp:DataList ID="DataList1" runat="server" DataKeyField="ITEM_CATEGORY_ID" DataSourceID="itemcatesds1" Width="100%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbxcate1" runat="server" Text='<%# Eval("ITEM_CATEGORY_NAME") %>' />
                                                <asp:HiddenField ID="hfItemCateId1" runat="server" Value='<%# Eval("ITEM_CATEGORY_ID") %>' />
                                            </ItemTemplate>
                                        </asp:DataList>
                                        <asp:SqlDataSource ID="itemcatesds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [ITEM_CATEGORY_ID], [ITEM_CATEGORY_NAME] FROM [YANTRA_LKUP_ITEM_CATEGORY] ORDER BY [ITEM_CATEGORY_NAME]"></asp:SqlDataSource>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <h2>
                                            <asp:Label ID="lblLocationName1" runat="server" Text=""></asp:Label></h2>
                                        <asp:HiddenField ID="hfwhlocid1" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="PnlAddNewLocation1" runat="server">
                                            <table>
                                                <tr>
                                                    <td>Add Location / Sub Location:</td>
                                                    <td>
                                                        <asp:TextBox ID="tbxLocationName1" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnaddLocation1" runat="server" OnClick="btnaddLocation1_Click" Text="Add Location" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>

    </table>
    <table style="width: 100%">
        <tr style="width: 100%">
            <td><span class="subh1">Manage Warehouse Sub Locations</span></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="whLocId" DataSourceID="warehousesds1" Width="100%" OnRowDeleted="GridView1_RowDeleted" OnRowUpdated="GridView1_RowUpdated">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="whLocId" HeaderText="Sub Loc Id" ReadOnly="True" SortExpression="whLocId" />
                        <asp:BoundField DataField="whLocName" HeaderText="Sub Location Name" SortExpression="whLocName" />
                        <asp:BoundField DataField="whname" HeaderText="whaddr" SortExpression="whname" />
                        <asp:BoundField DataField="locname" HeaderText="whdesc" SortExpression="locname" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="warehousesds1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DBCon %>" DeleteCommand="DELETE FROM [WH_Locations] WHERE [whLocId] = @original_whLocId"  OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT dbo.WH_Locations.whLocId, dbo.WH_Locations.whLocName, dbo.warehouse_tbl.whname, dbo.location_tbl.locname FROM dbo.location_tbl INNER JOIN dbo.warehouse_tbl ON dbo.location_tbl.locid = dbo.warehouse_tbl.locid INNER JOIN dbo.WH_Locations ON dbo.warehouse_tbl.wh_id = dbo.WH_Locations.wh_id order by WH_Locations.whLocId desc" UpdateCommand="UPDATE [WH_Locations] SET [whLocName] = @whLocName WHERE [whLocId] = @original_whLocId AND (([whLocName] = @original_whLocName) OR ([whLocName] IS NULL AND @original_whLocName IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_whLocId" Type="String" />
                        <asp:Parameter Name="original_whLocName" Type="String" />
                        <asp:Parameter Name="original_whname" Type="String" />
                        <asp:Parameter Name="original_locname" Type="String" />
                    </DeleteParameters>
                                                           
                    <UpdateParameters>
                        <asp:Parameter Name="whLocName" Type="String" />
                        
                        <asp:Parameter Name="original_whLocId" Type="String" />
                        <asp:Parameter Name="original_whLocName" Type="String" />
                        
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
