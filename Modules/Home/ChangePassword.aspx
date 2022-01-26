<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Modules_Home_ChangePassword" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="750">
    <tr>
                                <td colspan="7" class="profilehead">
                                    change password</td>
    </tr>
    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
        <td colspan="5" style="text-align: center" >
            <table border="0" cellpadding="0" cellspacing="0"  style="text-align: left">
                <tr>
                    <td align="center" class="table" colspan="2">
                        <br />
                        <table id="tabPerDet" runat="server" align="center" contenteditable="true"
                            visible="true">
                            <tr>
                                <td rowspan="1" style=" height: 21px; text-align: right">
                                    <asp:Label ID="Label12" runat="server" CssClass="label" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="8pt" ForeColor="#000040" Text="Enter Old Password:"></asp:Label></td>
                                <td colspan="3" rowspan="1" style="height: 21px; text-align: left">
                                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="textbox" Font-Names="Verdana"
                                        MaxLength="30" TabIndex="1" TextMode="Password" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                                        ErrorMessage="Please Enter Old Password">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="height: 21px; text-align: right">
                                </td>
                                <td colspan="2" style="height: 21px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1" style=" text-align: right">
                                    <asp:Label ID="Lbl_new" runat="server" CssClass="label" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="8pt" ForeColor="#000040" Text="Enter New Password:" Width="134px"></asp:Label></td>
                                <td colspan="3" rowspan="1" style="height: 11px; text-align: left">
                                    <asp:TextBox ID="txtNewPass" runat="server" CssClass="textbox" Font-Names="Verdana"
                                        MaxLength="30" TabIndex="1" TextMode="Password" Width="150px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPass"
                                        ErrorMessage="Please Enter New Password">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="height: 11px; text-align: right">
                                </td>
                                <td colspan="2" style="height: 11px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="1" style=" text-align: right">
                                    <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="8pt" ForeColor="#000040" Text="Retype New Password:" Width="140px"></asp:Label></td>
                                <td colspan="3" rowspan="1" style="text-align: left">
                                    <asp:TextBox ID="txtRetypeNewpassword" runat="server" CssClass="textbox" Font-Names="Verdana"
                                        MaxLength="30" TabIndex="1" TextMode="Password" Width="150px"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPass"
                                        ControlToValidate="txtRetypeNewpassword" ErrorMessage="New Password Not Matching">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRetypeNewpassword"
                                        ErrorMessage="Please Confirm Password">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right">
                                </td>
                                <td colspan="2" style="text-align: left">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" rowspan="4">
                        <table align="center" style=" height: 4px">
                            <tr>
                                <td style="height: 61px; text-align: center">
                                    <asp:Button ID="btnSave" runat="server"
                                        EnableTheming="True" Font-Bold="False" 
                                        OnClick="btnSave_Click" TabIndex="17" Text="Submit" Width="96px" /></td>
                                <td style="height: 61px; text-align: center">
                                    <asp:Button ID="btnCancel" runat="server" BorderStyle="None" CausesValidation="False" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                         OnClick="btnCancel_Click" TabIndex="18" Text="Refresh" Width="96px" /></td>
                                <td style=" height: 61px; text-align: center">
                                    <asp:Button ID="btnClose" runat="server" BorderStyle="None" CausesValidation="False" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                         OnClick="btnClose_Click" TabIndex="19" Text="Close" Width="96px" Visible="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
</table>

</asp:Content>


 
