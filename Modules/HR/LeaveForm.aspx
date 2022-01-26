<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HR/HRMaster.master" AutoEventWireup="true" CodeFile="LeaveForm.aspx.cs" Inherits="HR_LeaveForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table id="TblMain" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height:30px;font-size:16px"><b>Leave Form</b></td>
        </tr>
        <tr>
            <td colspan="2" align ="center">
                <asp:GridView ID="gvLeaveForm" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" AllowPaging="True" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Leave_Id" HeaderText="Slno" />
                        <asp:TemplateField HeaderText="Employee Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnLeave" runat="server" OnClick="lbtnLeave_Click" ForeColor="Blue" Text="<%# Bind('EMP_FIRST_NAME') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />
                        <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="ReasonforLeave" HeaderText="Reason" />
                        <asp:BoundField DataField="APPROVEDBY" HeaderText="Approved By" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_Leave_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
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
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" runat="server" visible ="false" id="tblDetails">
                    <tr>
                        <td colspan="4" class="profilehead">Leave Form Details</td>
                    </tr>
                    <tr>
                        <td style="width: 160px">&nbsp;</td>
                        <td style="width: 148px">&nbsp;</td>
                        <td align ="right" class="auto-style1">
                            <asp:Label ID="Label5" runat="server" Text="Date :"></asp:Label>
                        </td>
                        <td style="width: 152px">
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label3" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 148px">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align ="right" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="EmployeeName"></asp:Label>
                        </td>
                        <td align ="left" style="width: 152px">
                            <asp:DropDownList ID="ddlEmployee" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label2" runat="server" Text="Designation :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 148px">
                            <asp:DropDownList ID="ddlDesignation" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align ="right" class="auto-style1">
                            <asp:Label ID="Label4" runat="server" Text="No of Days Leave :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 152px">
                            <asp:TextBox ID="txtNoofdaysleave" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label6" runat="server" Text="From Date :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 148px">
                            <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                             <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" Enabled="True"
                                PopupButtonID="Image1" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                          
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Calendar.png" meta:resourcekey="imgDoaResource1" />
                        </td>
                        <td align ="right" class="auto-style1">
                            <asp:Label ID="Label8" runat="server" Text="To Date :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 152px">
                            <asp:TextBox ID="txtTodate" runat="server"></asp:TextBox>
                              <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" Enabled="True"
                                PopupButtonID="Image2" TargetControlID="txtTodate">
                            </cc1:CalendarExtender>
                           
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Calendar.png" meta:resourcekey="imgDoaResource1" />
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label12" runat="server" Text="Reason for ApplyLeave :"></asp:Label>
                        </td>
                        <td align ="left" colspan="3">
                            <asp:TextBox ID="txtReasonforapply" runat="server" TextMode="MultiLine" Width="377px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label17" runat="server" Text="Address in Leave Period :"></asp:Label>
                        </td>
                        <td align ="left" colspan="3">
                            <asp:TextBox ID="txtAddressinLeavePeriod" runat="server" TextMode="MultiLine" Width="376px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" colspan="4" style="text-align: left">
                            Number Of Leaves Available</td>
                    </tr>
                    <tr>
                        <td align ="right" colspan="4" style="text-align: left">
                            <table class="stacktable">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label19" runat="server" Text="Casual Leaves :"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblCasualLeaves" runat="server"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label20" runat="server" Text="Sick Leaves :" Visible="False"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblSickleaves" runat="server" Visible="False"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label21" runat="server" Text="Leaves Earned :"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblLeavesEarned" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" colspan="4" style="text-align: left">
                            Charge Handed Over To</td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label15" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 148px">
                            <asp:DropDownList ID="ddlChargeDept" runat="server" OnSelectedIndexChanged="ddlChargeDept_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td align ="right" class="auto-style1">
                            <asp:Label ID="Label16" runat="server" Text="EmployeeName"></asp:Label>
                        </td>
                        <td align ="left" style="width: 152px">
                            <asp:DropDownList ID="ddlChargeEmployee" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align ="right" style="width: 160px">
                            <asp:Label ID="Label18" runat="server" Text="Approved By :"></asp:Label>
                        </td>
                        <td align ="left" style="width: 148px">
                            <asp:DropDownList ID="ddlApprovedby" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label22" runat="server" Text="Leave Type :"></asp:Label>
                        </td>
                        <td style="width: 152px">
                            <asp:DropDownList ID="ddlLeaveType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                <asp:ListItem Value="0">----</asp:ListItem>
                                <asp:ListItem Value="SickLeaves">SICK LEAVE</asp:ListItem>
                                <asp:ListItem Value="CasualLeaves">CASUAL LEAVE</asp:ListItem>
                                <asp:ListItem Value="LeavesEarned">EARNED LEAVE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
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
</asp:Content>


