<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OvertimeRule.aspx.cs" Inherits="Modules_HRManagement_OvertimeRule" Title="|| YANTRA : HR Management : Over Time Rule ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="pagehead">
        <tr>
            <td colspan="2">
                ADDING OVERTIME DETAILS</td>
        </tr>
    </table>
    <table style="width: 510px; height: 116px">
        <tr>
            <td colspan="2" style="height: 23px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left" class="profilehead">
                &nbsp;OverTime Rule Details</td>
        </tr>
        <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                
                <asp:Label id="lblDate" runat="server" Text="Date Of Implementaion Of Rule For  OverTime" 
                    ></asp:Label></td>
            <td style="text-align: left">
                <asp:TextBox id="txtDate" runat="server" Width="125px"></asp:TextBox><asp:Image id="Image2" runat="server" ImageUrl="~/Images/Calendar.png">
                </asp:Image></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="Label2" runat="server" Text="Maximum Overtime Allowed In a Month"
                    ></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtMaxHrs" runat="server" Width="58px" CssClass="textbox" EnableTheming="False" ></asp:TextBox><asp:Label id="lblHrs" runat="server" Text="Hrs" ></asp:Label><asp:TextBox id="txtMaxMin" runat="server" Width="57px" CssClass="textbox" EnableTheming="False"  ></asp:TextBox><asp:Label id="lblMin" runat="server" Text="Min"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="Label3" runat="server" Text="Minimum Time Considered to Calculate Overtime"
                    ></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtMinHrs" runat="server" Width="59px" CssClass="textbox" EnableTheming="False" ></asp:TextBox><asp:Label id="Label4" runat="server" Text="Hrs"></asp:Label><asp:TextBox id="txtMinimumMin" runat="server" Width="57px" CssClass="textbox" EnableTheming="False" ></asp:TextBox><asp:Label id="Label5" runat="server" Text="Min"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 22px;">
                <asp:Label id="Label6" runat="server" Text="Multiple Of Extra Hour Of Working After Prescribed Minimum  time, Considered  to Calculate Overtime (For ex Minimum Time Prescribed in 1hr , then enter the Duration of time slot after which the overtime to be caluculated)   " Width="472px" 
                    ></asp:Label></td>
            <td rowspan="1" style="text-align: left; height: 22px;">
                <asp:TextBox id="txtPrescribedHrs" runat="server" Width="59px" CssClass="textbox" EnableTheming="False"></asp:TextBox><asp:Label id="Label8" runat="server" Text="Hrs"></asp:Label><asp:TextBox
                    id="TextBox1" runat="server" Width="53px" CssClass="textbox" EnableTheming="False"></asp:TextBox><asp:Label id="Label9" runat="server" Text="Min"></asp:Label></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label id="Label10" runat="server" Text="Whether there is a fixed amount of overtime for each multiples" 
                    ></asp:Label></td>
            <td style="text-align: left;">
                <asp:DropDownList id="ddlFixedAmount" runat="server">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                &nbsp;<asp:Label id="Label11" runat="server" Text="Salary for Multiples of overtime" ></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtSalary" runat="server">
                </asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right; height: 21px;">
            </td>
            <td style="text-align: left; height: 21px;">
            </td>
        </tr>
        <tr>
            <td style="height: 26px; text-align: center;" colspan="2">
                <asp:Button id="btnSave" runat="server" Text="Save"  />&nbsp;<asp:Button
                    id="btnCancel" runat="server" Text="Cancel" /><br />
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

