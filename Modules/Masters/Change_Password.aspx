<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="Modules_Admin_Change_Password" Title="|| YANTRA : Admin : Change Paasword ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">


<table class="pagehead">
        <tr>
            <td >
                CHANGE USER DETAILS</td>
        </tr>
    </table>
    <br />
    <br />

<table border="0" cellpadding="0" cellspacing="0" style="width: 774px">
        <tr>
            <td class="searchhead" colspan="2" style="height: 21px; text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            User Details</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3" style="color: #ffffff">
                                        <asp:DropDownList id="ddlCurrentDayTaskSearchBy" runat="server" CssClass="textbox">
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        </td>
                                    <td rowspan="3">
                                        </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox></td>
                                    <td rowspan="3">
                                        <asp:Button id="btnCurrentDayTasksGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 21px; text-align: center">
                <asp:GridView ID="gvChangePassword" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" meta:resourcekey="gvItemTypeDetailsResource1">
                    <Columns>
<asp:BoundField HeaderText="UserNameHidden" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
<asp:TemplateField HeaderText="User Name" meta:resourceKey="TemplateFieldResource1"><EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TextBox1Resource1" Text='<%# Bind("It_type") %>'></asp:TextBox>
                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
<ItemTemplate>
                                <asp:LinkButton ID="lbtnUserName" runat="server" CausesValidation="False" meta:resourcekey="lbtnItemTypeNameResource1"></asp:LinkButton>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="Privelege" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
<asp:BoundField HeaderText="Start Date" meta:resourceKey="BoundFieldResource3">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField HeaderText="End Date"></asp:BoundField>
</Columns>
                    <EmptyDataTemplate>
                        No Record Found
                    
</EmptyDataTemplate>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 21px; text-align: center">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" /></td>
                        <td>
                            <asp:Button id="Button2" runat="server" Text="Edit" /></td>
                        <td>
                            <asp:Button id="Button11" runat="server" Text="Delete" /></td>
                        <td>
                            <asp:Button id="btnResetPwd" runat="server" Text="Reset Pwd" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    <tr>
        <td colspan="2" style="height: 21px; text-align: center">
            <table id="tblUserDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                style="width: 783px" visible="false">
                <tr>
                    <td id="tblItDetails" runat="server" colspan="2" style="height: 19px; text-align: right">
                        <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 783px"
                            visible="true">
                            <tr>
                                <td class="profilehead" colspan="2" style="height: 20px; text-align: left">
                General Details</td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                    <asp:Label id="lblEmployeeName" runat="server" Text="User Name" ></asp:Label></td>
                                <td style="height: 21px; text-align: left">
                                    <asp:DropDownList id="ddlUserName" runat="server" CssClass="textbox" Width="154px">
            </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="height: 24px; text-align: right">
                                    &nbsp; &nbsp;&nbsp;<asp:Label id="Label5" runat="server" Text="Old Password" ></asp:Label>
                                    &nbsp;
                                </td>
                                <td style="height: 24px; text-align: left">
                <asp:TextBox id="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                <asp:Label id="Label10" runat="server" Text="New Password"></asp:Label></td>
                                <td style="text-align: left"><asp:TextBox id="txtNewPassword" runat="server" TextMode="Password">
                                </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                <asp:Label id="Label11" runat="server" Text="Confirm New Password"></asp:Label></td>
                                <td style="text-align: left"><asp:TextBox id="txtConfirmNewPassword" runat="server" TextMode="Password">
                                </asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 19px; text-align: right">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 49px; text-align: center">
                        <table id="Table3">
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" meta:resourcekey="btnSaveResource1" Text="Save" /></td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" CausesValidation="False" meta:resourcekey="btnRefreshResource1" Text="Refresh" /></td>
                                <td>
                                    <asp:Button ID="Button4" runat="server" CausesValidation="False" meta:resourcekey="btnCloseResource1" Text="Close" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>

</asp:Content>


 
