<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckingFormat.ascx.cs" Inherits="Modules_widgets_CheckingFormat" %>

<table>
        <tr>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
               

                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblcpid" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>MRN Details :</strong>
<asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CHK_ID" DataSourceID="MRNsds" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="MRN NO" SortExpression="CHK_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("CHK_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink7" runat="server"  Text='<%# Eval("CHK_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CHK_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="MRN DATE" SortExpression="CHK_DATE" />
                        <asp:BoundField DataField="CHK_PO_NO" HeaderText="MRN PO NO" SortExpression="CHK_PO_NO" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="MRNsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_SCM_CHECKING_FORMAT_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                        <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>--%>
                        <%--<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>--%>
                    </SelectParameters>
                </asp:SqlDataSource>
