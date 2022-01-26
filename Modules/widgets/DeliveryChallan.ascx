<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DeliveryChallan.ascx.cs" Inherits="Modules_widgets_DeliveryChallan" %>
<table>
        <tr>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblcpid" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>DC Details :</strong>
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="DC_ID" DataSourceID="DCsds" OnRowDataBound="GridView4_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="DC NO" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DC_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("DC_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="DC DATE" SortExpression="DC_DATE" />
                        <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDCStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DC_FOR" HeaderText="DC FOR" SortExpression="DC_FOR" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="DCsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_INVENTORY_DELIVERY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EMPID" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblcpid" DefaultValue="0" Name="CpId" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>

