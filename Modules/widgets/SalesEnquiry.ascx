<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesEnquiry.ascx.cs" Inherits="Modules_widgets_SalesEnquiry" %>
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
<strong>Sales Lead :</strong>
<asp:GridView ID="gvSalesEnq" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ENQ_ID" DataSourceID="salesleadsds1" OnRowDataBound="gvSalesEnq_RowDataBound" Width="100%" AllowPaging="True" PageSize="5">
                    <Columns>
                        <asp:TemplateField HeaderText="Enquiry NO" SortExpression="ENQ_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ENQ_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("ENQ_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CUST_NAME" HeaderText="CUSTOMER NAME" SortExpression="CUST_NAME" />
                        <asp:TemplateField HeaderText=" STATUS" SortExpression="ENQ_STATUS">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ENQ_STATUS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEnqStatus1" runat="server" Text='<%# Bind("ENQ_STATUS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="salesleadsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_SM_SALESENQUIRY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="USERTYPE" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
