<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrderDetails.ascx.cs" Inherits="Modules_widgets_PurchaseOrderDetails" %>
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
<strong>Purchase Orders :</strong>
<asp:GridView ID="GridView6" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="FPO_ID" DataSourceID="Purchaseordersds" PageSize="5" Width="100%" OnRowDataBound="GridView6_RowDataBound1">
                    <Columns>
                        <asp:BoundField DataField="FPO_ID" HeaderText="ID" ReadOnly="True" SortExpression="FPO_ID" />
                        <asp:TemplateField HeaderText="PURCHSE ORDER NO." SortExpression="FPO_NO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("FPO_ID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink5" runat="server" Text='<%# Eval("FPO_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FPO_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="PURCHSE ORDER  DATE" SortExpression="FPO_DATE" />
                        <asp:TemplateField HeaderText="STATUS" SortExpression="FPO_PO_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("FPO_PO_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseStatus" runat="server" Text='<%# Bind("FPO_PO_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="Purchaseordersds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SCM_PurchaseOrder" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblCPID" DefaultValue="0" Name="CpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
