<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteInspection_Report.ascx.cs" Inherits="Modules_widgets_SiteInspection_Report" %>
<table>
        <tr>
            <td>
                <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
<strong>Site Inspection Reports :</strong>
<asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Client_Id" DataSourceID="ServiceRepsds" PageSize="5" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="CLIENT NAME" SortExpression="Client_Name">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Client_Id") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink3" runat="server" Text='<%# Eval("Client_Name") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Quotation_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText=" Quotation Date " SortExpression="Quotation_Date" />

                        <asp:BoundField DataField="Executive_Name" HeaderText=" EXECUTIVE NAME " SortExpression="Executive_Name" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="ServiceRepsds" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="select *  from Site_Inspection_Report_tbl order by Client_Id desc"></asp:SqlDataSource>

