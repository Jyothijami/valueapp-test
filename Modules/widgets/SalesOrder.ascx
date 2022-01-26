<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesOrder.ascx.cs" Inherits="Modules_widgets_SalesOrder" %>

<table>
        <tr>
            <td>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>Sales Orders :</strong>
<asp:GridView ID="GridView5" runat="server" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="SO_ID" DataSourceID="SalesOrdersds" OnRowDataBound="GridView5_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="PO NO" SortExpression="SO_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SO_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("SO_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SO_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PO DATE" SortExpression="SO_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="SO_ACCEPTANCE_FLAG">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("SO_ACCEPTANCE_FLAG") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPOStatus" runat="server" Text='<%# Bind("SO_ACCEPTANCE_FLAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SalesOrdersds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_SM_SALESORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="CpId" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>