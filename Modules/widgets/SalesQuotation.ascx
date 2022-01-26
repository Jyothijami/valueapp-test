<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesQuotation.ascx.cs" Inherits="Modules_widgets_SalesQuotation" %>
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
<strong>Sales Quotations :</strong>
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="QUOT_ID" DataSourceID="Quotationsds" OnRowDataBound="GridView4_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="QUOTATION NO" SortExpression="QUOT_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("QUOT_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("QUOT_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="QUOT_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="QUOTATION DATE" SortExpression="QUOT_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="QUOT_PO_FLAG">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("QUOT_PO_FLAG") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQuotStatus1" runat="server" Text='<%# Bind("QUOT_PO_FLAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Quotationsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SM_SALESQUOTATION_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
