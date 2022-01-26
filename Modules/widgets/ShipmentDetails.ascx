<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShipmentDetails.ascx.cs" Inherits="Modules_widgets_ShipmentDetails" %>
<table>
        <tr>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                    ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>Shipment Details ;</strong>
<asp:GridView ID="GridView8" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SD_ID" DataSourceID="Shipmentsds" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="SHIPMENT NO" SortExpression="SD_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("SD_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server" Text='<%# Eval("SD_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SD_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SHIPMENT DATE" SortExpression="SD_DATE" />
                        <asp:BoundField DataField="SUP_NAME" HeaderText="SUPPLIER NAME" SortExpression="SUP_NAME" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Shipmentsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SHIPINGDETAILS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
