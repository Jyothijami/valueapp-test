<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierMaster.ascx.cs" Inherits="Modules_widgets_SupplierMaster" %>
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
<strong>Supplier Master :</strong>
 <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SUP_ID" DataSourceID="Supplierinfosds" PageSize="5" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SUP_ID" HeaderText="ID" ReadOnly="True" SortExpression="SUP_ID" />
                        <asp:TemplateField HeaderText="Supplier Name" SortExpression="SUP_NAME">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SUP_NAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("SUP_NAME") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="BRAND" ReadOnly="True" SortExpression="PRODUCT_COMPANY_NAME" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Supplierinfosds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_SUPPLIER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
