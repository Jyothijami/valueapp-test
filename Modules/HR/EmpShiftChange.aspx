﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmpShiftChange.aspx.cs" Inherits="HR_EmpShiftChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" runat="Server">
    
    <table id="TblMain" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>Shift Change Form Pending Applications</b></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:GridView ID="gvShiftchange" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AutoGenerateColumns="False" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="Shift_Change_ID" HeaderText="Slno" />
                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnShiftChange" runat="server" OnClick="lbtnShiftChange_Click" ForeColor="#0066FF" Text="<%# Bind('EMP_FIRST_NAME') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="Shift_Change_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="Shift_Change_Date" />
                        <asp:BoundField DataField="Present_Shift" HeaderText="Present Shift" SortExpression="Present_Shift" />
                        <asp:BoundField DataField="Required_Shift" HeaderText="Required Shift" SortExpression="Required_Shift" />
                        <asp:BoundField DataField="Status1" HeaderText="Status" SortExpression="Status1" />
                    </Columns>
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_SHIFTCHANGE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" runat="server" visible="false" id="tblDetails">
                    <tr>
                        <td colspan="4" class="profilehead">Employee Shift Change</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDate" runat="server" type="date"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label3" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="Employee Name :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlEmployee" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label2" runat="server" Text="Designation :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDesignation" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="Location :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlLocation" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Present Shift Time :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPresentShift" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                Mask="99:99" MaskType="Date" TargetControlID="txtPresentShift" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Required Shift Time :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRequiredshift" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                Mask="99:99" MaskType="Date" TargetControlID="txtRequiredshift" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="Shift Change Between :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtshiftchangebetween" runat="server" TextMode="MultiLine" Width="455px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label15" runat="server" Text="Reason for Shift Change :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtReasonforshiftchange" runat="server" TextMode="MultiLine" Width="455px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="Approved By :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlApprovedby" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                                    </td>
                                    <td style="width: 4px">
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table id="TblMain2" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>Shift Change Approved/Rejected Applications</b></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="gvShiftchangeHist" runat="server" DataSourceID="SqlDataSource2" AllowPaging="True" AutoGenerateColumns="False" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="Shift_Change_ID" HeaderText="Slno" />
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="Shift_Change_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="Shift_Change_Date" />
                        <asp:BoundField DataField="Present_Shift" HeaderText="Present Shift" SortExpression="Present_Shift" />
                        <asp:BoundField DataField="Required_Shift" HeaderText="Required Shift" SortExpression="Required_Shift" />
                        <asp:BoundField DataField="Status3" HeaderText="Status" SortExpression="Status3" />
                    </Columns>
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_SHIFTCHANGE_SEARCH_SELECT_2" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                        <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EmpId" PropertyName="Text" Type="Int64" />
                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
</asp:Content>

