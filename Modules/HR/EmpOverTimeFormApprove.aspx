<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmpOverTimeFormApprove.aspx.cs" Inherits="Modules_HR_EmpOverTimeFormApprove" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table id="TblMain" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>Employee Over Time Form</b></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="gvOvertime" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="Overtime_ID" HeaderText="Slno" />
                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnOvertime" runat="server" OnClick="lbtnOvertime_Click" ForeColor="#0066FF" Text="<%# Bind('EMP_FIRST_NAME') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="Overtime_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="Overtime_Date" />
                        <asp:BoundField DataField="Worked_From_Time" HeaderText="From Time" DataFormatString="{0:HH/mm}" SortExpression="Worked_From_Time"/>
                        <asp:BoundField DataField="Worked_Upto_Time" HeaderText="To Time" DataFormatString="{0:HH/mm}"  SortExpression="Worked_Upto_Time"/>
                        <asp:BoundField DataField="Nature_Of_Work" HeaderText="Nature of Work" SortExpression="Nature_Of_Work" />
                        <asp:BoundField DataField="APPROVEDBY" HeaderText="Approved By"  SortExpression="APPROVEDBY"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_OVERTIME_Approve_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" Visible="False" />
                        </td>
                        <td>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%" runat="server" visible="false" id="tblDetails">
                    <tr>
                        <td colspan="4" class="profilehead">Employee Over Time Details</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDate" runat="server" type="date" ReadOnly="True"></asp:TextBox>
                            
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
                            <asp:Label ID="Label6" runat="server" Text="Worked From Time :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtfromtime" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                Mask="99:99" MaskType="Date" TargetControlID="txtfromtime" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Worked UpTo Time"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txttotime" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                Mask="99:99" MaskType="Date" TargetControlID="txttotime" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Coff Days :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCoffdays" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="Nature of Work :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtnatureofwork" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="Address of Place Worked :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtAddofplaceworked" runat="server" TextMode="MultiLine" Width="464px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="Remarks :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="464px"></asp:TextBox>
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
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Visible="False" Enabled="False" />
                                    </td>
                                    <td style="width: 4px">
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" Visible="False" />
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

