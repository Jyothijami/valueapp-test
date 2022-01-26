<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ShipmentStat.aspx.cs" Inherits="Modules_Reports_ShipmentStat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table id="tblShipment" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="profilehead" colspan="3" style="height: 20px; text-align: left">Shipment Details Statement</td>
                    </tr>
                    <tr>
                        <td style="height: 19px"></td>
                        <td style="height: 19px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label22" runat="server" Text="Company Name" Width="103px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlCompanyNameShipment" runat="server" AutoPostBack="True"
                                CausesValidation="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCompanyNameShipment"
                                ErrorMessage="Please Select the Company Name " InitialValue="0" ValidationGroup="shipment" Visible="False">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label23" runat="server" Text="From" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtShipmentFrom" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgShipmentFrom" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtShipmentFrom"
                                ErrorMessage="Please Select the From Date " ValidationGroup="shipment">*</asp:RequiredFieldValidator>
                            <%--<cc1:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgShipmentFrom" TargetControlID="txtShipmentFrom">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtShipmentFrom"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label24" runat="server" Text="To" Width="117px"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtShipmentTo" runat="server" CausesValidation="True" type="datepic" CssClass="datetext"
                                EnableTheming="False">
                            </asp:TextBox><%--<asp:Image ID="imgShipmentTo" runat="server" ImageUrl="~/Images/Calendar.png"></asp:Image>&nbsp;--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtShipmentTo"
                                ErrorMessage="Please Select the ToDate " InitialValue="0" ValidationGroup="shipment">*</asp:RequiredFieldValidator>
                            <%--  <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                PopupButtonID="imgShipmentTo" TargetControlID="txtShipmentTo">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" DisplayMoney="Left"
                                Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtShipmentTo"
                                UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label25" runat="server" Text="Brand" Width="117px" ></asp:Label></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlBrand1" runat ="server"></asp:DropDownList> </td> 
                            </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="Label26" runat="server" Text="Employee Name" Width="117px" Visible="False"></asp:Label></td>
                        <td style="text-align: left"></td> 
                             </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="2" style ="text-align :center ">
                            <asp:Button ID="btnShipment" runat="server" Text="Run Report"
                                ValidationGroup="shipment" OnClick="btnShipment_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;<asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="shipment"></asp:ValidationSummary>
                        </td>
                    </tr>
                </table>
</asp:Content>

