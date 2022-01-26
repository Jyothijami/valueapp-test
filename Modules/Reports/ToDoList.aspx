<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ToDoList.aspx.cs" Inherits="Modules_Reports_DailyReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
     <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left">To-Do List Report</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblDailyReport" runat="server">
                    <tr>
                        <td class="profilehead" colspan="3" style="text-align: left; height: 20px;">To-Do List Report</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label43" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtFromDailyRep" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgFromDaily" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtFromDailyRep"
                                    ErrorMessage="Please Select  Date" ValidationGroup="SalesLead">*</asp:RequiredFieldValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender14" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgFromDaily"
                                            TargetControlID="txtFromDailyRep">
                                    </cc1:CalendarExtender>--%>
                            <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender17" runat="server" DisplayMoney="Left" Enabled="True"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDailyRep" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label44" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtToDailyRep" runat="server" type="datepic" CssClass="datetext" EnableTheming="False">
                            </asp:TextBox><%--<asp:Image
                                ID="imgToDaily" runat="server" ImageUrl="~/Images/Calendar.png" />--%><asp:CustomValidator
                                    ID="CustomValidator14" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtToDailyRep" ErrorMessage="Please Enter the TO  Daily Report Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True" ValidationGroup="SalesLead">*</asp:CustomValidator><%--<cc1:CalendarExtender
                                            ID="CalendarExtender15" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgToDaily"
                                            TargetControlID="txtToDailyRep">
                                    </cc1:CalendarExtender>--%>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender18" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSalesLeadTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label45" runat="server" Text="Department"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlDepDaily" runat="server" AutoPostBack="True" meta:resourcekey="ddlDepartmentResource1"
                                OnSelectedIndexChanged="ddlDepDaily_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label46" runat="server" Text="Employee Name" ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlEmpDaily" runat="server" Width="147px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2"  style="height: 23px; text-align :center ">
                            <asp:Button ID="runDailyReport" runat="server" OnClick="runDailyReport_Click" Text="Run Report" ValidationGroup="SalesLead" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="SalesLead" />
                        </td>
                    </tr>
                </table>
    <asp:Label ID="lblUserId" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblEmpId" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblDeptId" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
                <asp:Label ID="lblDeptHead" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                    Visible="False"></asp:Label>
</asp:Content>

