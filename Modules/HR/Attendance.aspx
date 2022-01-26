<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Attendance.aspx.cs" Inherits="Modules_HRManagement_Attendance" Title="|| YANTRA : HR : Attendance ||" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table width="100%" id="TABLE1">
        <tr>
            <td class="pagehead" colspan="3">
                attendance details</td>
        </tr>
        <tr>
            <td colspan="3" style="text-align: center">
                <table>
                    <tr>
                        <td colspan="2" style="height: 20px; text-align: left">
                        </td>
                    </tr>
                    <tr>
            <td colspan="2" style="text-align: left" class="profilehead">
                General Details</td>
                    </tr>
                    <tr>
            <td colspan="2" style="height: 21px">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <%--<asp:Label id="Label1" runat="server" Text="Whether there  is a limit for maximum late arrival for  job or early departure from job in a single day "
                    Width="435px"></asp:Label>--%>
                Whether there  is a limit for maximum late<br /> arrival for  job or early departure from job in a single day 
            </td>
            <td style="text-align: left">
                <asp:DropDownList id="ddlMaximumLateArrival" runat="server">
                </asp:DropDownList></td>
                    </tr>
                    <tr>
            <td style="height: 23px; text-align: right;">
                <%--<asp:Label id="Label2" runat="server" Text="Buffer for short/ late attendance in minutes"></asp:Label>--%>
                Buffer for short/ late attendance in minutes
            </td>
            <td style="height: 23px; text-align: left;">
                <asp:TextBox id="txtBuffer" runat="server">
                </asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="height: 23px; text-align: right;">
                <%--<asp:Label id="Label3" runat="server" Text="Maximum no. of days allowed in a month"></asp:Label>--%>
                Maximum no. of days allowed in a month
            </td>
            <td style="height: 23px; text-align: left;">
                <asp:TextBox id="txtMaximumDays" runat="server">
                </asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="height: 23px; text-align: right;">
                <%--<asp:Label id="Label4" runat="server" Text="If employee exceeds maximum number of days for late arrival  or early departure should the same be considerd as absent for half day"
                    Width="419px"></asp:Label>--%>
                If employee exceeds maximum number of days for <br /> late arrival  or early departure should the same be considerd as absent for half day
            </td>
            <td style="height: 23px; text-align: left;">
                <asp:DropDownList id="ddlMinimumLateArrival" runat="server">
                </asp:DropDownList></td>
                    </tr>
                    <tr>
            <td style="height: 23px">
            </td>
            <td style="height: 23px">
            </td>
                    </tr>
                    <tr>
            <td colspan="2" style="height: 26px">
                <asp:Button id="btnSave" runat="server" Text="Save" Width="59px" />
                <asp:Button id="btnCancel"
                    runat="server" Text="Cancel" /><br />
                <br />
            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


 
