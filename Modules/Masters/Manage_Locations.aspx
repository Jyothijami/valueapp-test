<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Manage_Locations.aspx.cs" Inherits="Modules_Masters_Manage_Locations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td><span class="subh1">Add New Warehouse Location</span></td>
        </tr>
        <tr>
            <td>
                <table cellpadding="5" cellspacing="5">
                    <tr>
                        <td>Location:</td>
                        <td>
                            <asp:TextBox ID="tbxLocationName1" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Description:</td>
                        <td>
                            <asp:TextBox ID="txtDesc" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnAddLocation1" runat="server" OnClick="btnAddLocation1_Click" Text="Add Location" />
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
            <td><span class="subh1">Manage Locations</span></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="locid" DataSourceID="locsds1" >
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                        <asp:BoundField DataField="locid" HeaderText="locid" ReadOnly="True" SortExpression="locid" />
                        <asp:BoundField DataField="locname" HeaderText="Location" SortExpression="locname" />
                        <asp:BoundField DataField="Description" HeaderText="Location Description" SortExpression="Description" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="locsds1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DBCon %>" 
                    DeleteCommand="DELETE FROM [location_tbl] WHERE [locid] = @original_locid AND (([locname] = @original_locname) OR ([locname] IS NULL AND @original_locname IS NULL))"
                     InsertCommand="INSERT INTO [location_tbl] ([locid], [locname]) VALUES (@locid, @locname)" OldValuesParameterFormatString="original_{0}" 
                    SelectCommand="SELECT [locid], [locname],[Description] FROM [location_tbl] ORDER BY [locname]" 
                    UpdateCommand="UPDATE [location_tbl] SET [locname] = @locname,[Description]=@Description
 WHERE [locid] = @original_locid">
                    <DeleteParameters>
                        <asp:Parameter Name="original_locid" Type="String" />
                        <asp:Parameter Name="original_locname" Type="String" />
                        <asp:Parameter Name="original_locDesc" Type="String" />

                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="locid" Type="String" />
                        <asp:Parameter Name="locname" Type="String" />
                        <asp:Parameter Name="original_locDesc" Type="String" />

                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="locname" Type="String" />
                        <asp:Parameter Name="Description" />
                        <asp:Parameter Name="original_locid" Type="String" />
                        <asp:Parameter Name="original_locname" Type="String" />
                        <asp:Parameter Name="original_locDesc" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
