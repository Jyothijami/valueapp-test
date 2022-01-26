<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/AdminMP1.master" AutoEventWireup="true" CodeFile="Attendance_Report.aspx.cs" Inherits="Modules_Masters_Attendance_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table style="width: 100%">
        <tr>
            <td colspan="4" class="profilehead">Attendence Details of Lastest Date </td>

        </tr>
        <tr>
            <td colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align:right">Employee Name :
            </td>
            <td colspan="3" style="text-align:left">
                <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right">Location :
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="2">Bangalore</asp:ListItem>
                    <asp:ListItem Value="1">Hyderabad</asp:ListItem>
                    <asp:ListItem Value="4">Mumbai</asp:ListItem>
                    <asp:ListItem Value="3">Vijayawada</asp:ListItem>
                    <asp:ListItem Value="5">Vizag</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right" class="auto-style1">Total Time : 
            </td>
            <td colspan="3" class="auto-style1">
                <asp:TextBox ID="txtTotalTime" Width="50px" runat="server"></asp:TextBox>
                <asp:Button ID="btnTotalGo" runat="server" Width="30px" Text="GO" OnClick="btnTotalGo_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right">In Time : 
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtInTime" Width="50px" runat="server" Visible="False"></asp:TextBox>
                <asp:Button ID="btnInGo" runat="server" Width="30px" Text="GO" OnClick="btnInGo_Click" />

            </td>
        </tr>
        <tr>
            <td style="text-align: right">Out Time : 
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtOutTime" Width="50px" runat="server" Visible="False"></asp:TextBox>
                <asp:Button ID="btnOutGo" runat="server" Width="30px" Text="GO" OnClick="btnOutGo_Click" />

            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:GridView ID="gdvAttendence" runat="server" Width="100%">
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>


 
