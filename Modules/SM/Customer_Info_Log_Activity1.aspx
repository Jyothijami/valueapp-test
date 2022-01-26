<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="Customer_Info_Log_Activity1.aspx.cs" Inherits="Modules_SM_Customer_Info_Log_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
<table style="width:100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" Visible="true" runat="server" Text="Select User :"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlUser1" Visible="true" runat="server" AppendDataBoundItems="True" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                <asp:ListItem Value="0">-- Select / Show All --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] ORDER BY [USER_NAME]"></asp:SqlDataSource>
                        </td>
                        
                        <td>
                            No Of Records :
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                   
                </asp:DropDownList>

                        </td>
                        <td>
                            <asp:Button ID="btnShow1" runat="server" Visible="true" OnClick="btnShow1_Click" Text="Show" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                                
                <br />

                <asp:GridView ID="GridView1" runat="server" Visible="true" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="logid" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="logid" HeaderText="logid" ReadOnly="True" SortExpression="logid" />
                        <asp:TemplateField HeaderText="Description" SortExpression="logdesc">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("logdesc") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("USER_NAME") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("logtype", "{0}ed") %>'></asp:Label>
                                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("logdesc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name" SortExpression="logcatename">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("logcatename") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("logcatename") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Change"  SortExpression="dt_added">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"  Text='<%# Bind("dt_added","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server"  Text='<%# Bind("dt_added","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

               
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
