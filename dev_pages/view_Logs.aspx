<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="view_Logs.aspx.cs" Inherits="dev_pages_view_Logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
    <tr>
        <td>
            <h1>Server Logs</h1></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnrefresh1" runat="server" OnClick="btnrefresh1_Click" Text="Refresh" />
        </td>
    </tr>
    <tr>
        <td>
            <table style="width:100%;">
                <tr>
                    <td style="vertical-align: top">
                                <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Vertical">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                    <asp:GridView ID="gvDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="gvDetails_RowCommand" PageSize="100">
                                        <Columns>
                                            <asp:TemplateField HeaderText="File" SortExpression="fname">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("fname","~/error_log_files/{0}") %>' Text='<%# Eval("fname") %>'></asp:HyperLink>
                                                    <br />
                                                    <asp:Label ID="Label1" runat="server" ForeColor="#FF9966" Text='<%# Eval("createdOn") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnOpen1" runat="server" CommandArgument='<%# Eval("fname","~/error_log_files/{0}") %>' CommandName="Open" Text="&gt;&gt;" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                                </asp:Panel>
                    </td>
                    <td style="vertical-align: top">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel2" runat="server" Height="500px" ScrollBars="Vertical">
                                    <asp:Literal ID="txtLit1" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
</asp:Content>


 
