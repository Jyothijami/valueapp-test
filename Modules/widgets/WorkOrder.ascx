<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WorkOrder.ascx.cs" Inherits="Modules_widgets_WorkOrder" %>
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
<strong>Internal Orders :</strong>
<asp:GridView ID="GridView6" AllowSorting="True" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="WO_ID" DataSourceID="WorkOrdersds" OnRowDataBound="GridView6_RowDataBound" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="IO NO" SortExpression="WO_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("WO_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("WO_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WO_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="IO DATE" SortExpression="WO_DATE" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="WO_FLAG">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("WO_FLAG") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIOStatus" runat="server" Text='<%# Bind("WO_FLAG") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="WorkOrdersds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_SM_WORKORDER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
