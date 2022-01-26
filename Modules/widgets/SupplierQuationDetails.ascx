<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierQuationDetails.ascx.cs" Inherits="Modules_widgets_SupplierQuationDetails" %>
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
<strong>Supplier Enquiry :</strong>
<asp:GridView ID="GridView4" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="IND_APPRL_ID" DataSourceID="SupQuotationsds"  PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="SUPPLIER ENQUIRY NO" SortExpression="IND_APPRL_NO">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("IND_APPRL_ID") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("IND_APPRL_NO") %>'></asp:HyperLink>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IND_APPRL_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SUPPLIER ENQUIRY DATE" SortExpression="IND_APPRL_DATE" />
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="FollowUp Name" SortExpression="EMP_FIRST_NAME" />
                                              
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SupQuotationsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_Supenq_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
