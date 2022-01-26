<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesReturn.ascx.cs" Inherits="Modules_widgets_SalesReturn" %>

<table>
        <tr>
            <td>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>Sales Returns :</strong>
<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="SR_ID" DataSourceID="SalesReturnsds" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="SALES RETURN NO" SortExpression="SR_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("SR_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DC NO" SortExpression="DC_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SR_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("SR_ID", "~/Modules/SCM/SalesReturnDetails.aspx?SrNo={0}") %>' Text='<%# Eval("DC_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SR_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SALES RETURN DATE" SortExpression="SR_DATE" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SalesReturnsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_INVENTORY_SALESRETURN_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EMPID" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
