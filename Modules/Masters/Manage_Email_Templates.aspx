<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/emailTemplatesMP1.master" AutoEventWireup="true" CodeFile="Manage_Email_Templates.aspx.cs" Inherits="Modules_Masters_Manage_Email_Templates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Modules/Masters/create_email_Template.aspx">Create Email Template</asp:HyperLink>
<br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="templateid" DataSourceID="emailtempssds1">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="/n_Images/metro.icons/Details_24x24.png" NavigateUrl='<%# Eval("templateid", "ad_view_Email_Template.aspx?tid={0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="t_name" HeaderText="t_name" SortExpression="t_name" />
            <asp:BoundField DataField="t_subj" HeaderText="t_subj" SortExpression="t_subj" />
            <asp:BoundField DataField="dt_added" HeaderText="dt_added" SortExpression="dt_added" />
            <asp:BoundField DataField="templateid" HeaderText="templateid" ReadOnly="True" SortExpression="templateid" />
            <asp:BoundField DataField="shortid" HeaderText="shortid" SortExpression="shortid" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="emailtempssds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [t_name], [t_subj], [dt_added], [templateid], [shortid] FROM [email_templates] ORDER BY [t_name]"></asp:SqlDataSource>
</asp:Content>


 
