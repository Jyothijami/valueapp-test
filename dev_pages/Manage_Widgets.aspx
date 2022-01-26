<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MPage1.master" AutoEventWireup="true" CodeFile="Manage_Widgets.aspx.cs" Inherits="dev_pages_Manage_Widgets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>Add Widget</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>Widget Name:</td>
                        <td>
                            <asp:TextBox ID="tbxWName1" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>Path:</td>
                        <td>
                            <asp:TextBox ID="tbxWPath1" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>User Types:</td>
                        <td>
                            <asp:CheckBoxList ID="cbxList1" runat="server" DataSourceID="usertypesds1" DataTextField="userTypeName" DataValueField="userTypeId">
                            </asp:CheckBoxList>
                            <asp:SqlDataSource ID="usertypesds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [userTypeId], [userTypeName] FROM [usertype_tbl]"></asp:SqlDataSource>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSubmit1" runat="server" OnClick="btnSubmit1_Click" Text="Submit" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


 
