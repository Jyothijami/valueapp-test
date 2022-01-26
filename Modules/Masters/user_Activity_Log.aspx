<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="user_Activity_Log.aspx.cs" Inherits="Modules_Masters_user_Activity_Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Select User :</td>
                        <td>
                            <asp:DropDownList ID="ddlUser1" runat="server" AppendDataBoundItems="True" DataSourceID="usersds1" DataTextField="USER_NAME" DataValueField="USER_ID">
                                <asp:ListItem Value="0">-- Select / Show All --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="usersds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [USER_ID], [USER_NAME] FROM [YANTRA_USER_DETAILS] ORDER BY [USER_NAME]"></asp:SqlDataSource>

                        </td>
                        <td>User Name :
                            
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>


                        </td>
                    </tr>

                    <tr>
                        <td>Log Description :</td>
                        <td>
                            <asp:TextBox ID="txtCatName" runat="server"></asp:TextBox>
                        </td>
                        <td>Description ID :</td>
                        <td>
                            <asp:TextBox ID="txtCatId" runat="server"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td>From Date:</td>
                        <td>
                            <asp:TextBox ID="txtFDate" type="datepic" runat="server"></asp:TextBox>
                        </td>
                        <td>To Date :</td>
                        <td>
                            <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox></td>

                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnShow1" runat="server" OnClick="btnShow1_Click" Text="Search" />

                        </td>
                        <td>No.Of Records :</td>
                        <td>
                            <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>75</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>

                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="userActivitysds1"   runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="sp_getActivityLog_All" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                <asp:GridView ID="GridView2" Width="100%" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="logid" PageSize="30" OnPageIndexChanging="GridView2_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="logid" HeaderText="Log Id" ReadOnly="True" SortExpression="logid" />
                        <asp:BoundField DataField="USER_NAME" HeaderText="USER_NAME" SortExpression="USER_NAME" />
                        <asp:BoundField DataField="logdesc" HeaderText="Log Description" SortExpression="logdesc" />
                        <asp:BoundField DataField="logtypeid" HeaderText="Type Id" Visible="false" SortExpression="logtypeid" />
                        <asp:BoundField DataField="logtype" HeaderText="Type Of Activity" SortExpression="logtype" />
                        <asp:BoundField DataField="logcateid" HeaderText="logcateid" Visible="false" SortExpression="logcateid" />
                        <asp:BoundField DataField="logcatename" HeaderText="Module Category" SortExpression="logcatename" />
                        <asp:BoundField DataField="USER_ID" HeaderText="USER_ID" Visible="false" SortExpression="USER_ID" />
                        <asp:BoundField DataField="dt_added" HeaderText="Date" SortExpression="dt_added" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />
                    </Columns>
                </asp:GridView>

                <asp:DropDownList ID="ddlNoOfRecords1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords1_SelectedIndexChanged" Visible="False">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>

                </asp:DropDownList>

                <br />

                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="logid" OnPageIndexChanging="GridView1_PageIndexChanging">
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
                        <asp:TemplateField HeaderText="Module" SortExpression="logcatename">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("logcatename") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("logcatename") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="dt_added" SortExpression="dt_added">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("dt_added") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("dt_added", "{0:d}") %>'></asp:Label>
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



