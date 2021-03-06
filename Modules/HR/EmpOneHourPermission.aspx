<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmpOneHourPermission.aspx.cs" Inherits="HR_EmpOneHourPermission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" runat="Server">
    <%--  <script>
        $(function () {
            $("[name$='txtDate']").datepicker();
        });
    </script>--%>
    <table id="TblMain" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>One Hour Permission Pending Applications</b></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="gvOnehourpermission" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="One_Hour_ID" HeaderText="Slno" />
                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnOnehour" runat="server" ForeColor="#0066FF" OnClick="lbtnOnehour_Click" Text="<%# Bind('EMP_FIRST_NAME') %>"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="OneHour_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Of Apply" HtmlEncode="False" SortExpression="OneHour_Date" />
                        <asp:BoundField DataField="Req_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Required Date" HtmlEncode="False" SortExpression="Req_Date" />
                        <asp:BoundField DataField="Req_From_Time" HeaderText="From Time" SortExpression="Req_From_Time" />
                        <asp:BoundField DataField="Req_To_Time" HeaderText="To Time" SortExpression="Req_To_Time" />
                        <asp:BoundField DataField="Status1" HeaderText="Status" SortExpression="Status1" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ONEHOURPERMISSION_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
                        <td class="profilehead" colspan="4">Employee One Hour Permission</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                         <td style="text-align:right">
                            <asp:Label ID="lblReqDate" runat="server" Text="Required Date :"></asp:Label>

                        </td>
                        <td>
                            <asp:TextBox ID="txtReqDate" runat="server" type="datepic"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label3" runat="server" Text="Department :"></asp:Label>
                        </td>
                        <td align="left" class="auto-style1">
                            <asp:DropDownList ID="ddlDepartment" runat="server" Enabled="False" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </td>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="Label1" runat="server" Text="EmployeeName"></asp:Label>
                        </td>
                        <td align="left" class="auto-style1">
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
                            <asp:Label ID="Label6" runat="server" Text="Required From Time :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRequiredfromtime" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                Mask="99:99" MaskType="Date" TargetControlID="txtRequiredfromtime" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Required To Time :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtrequiredtotime" runat="server"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                Mask="99:99" MaskType="Date" TargetControlID="txtrequiredtotime" UserDateFormat="None" Enabled="True">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Reason for Permission :"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtReasonforpermission" runat="server" TextMode="MultiLine" Width="454px"></asp:TextBox>
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
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="Date Of Apply :" Visible="False"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDate" runat="server" type="datepic" Enabled="False" Visible="False"></asp:TextBox>

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
                                        <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" Visible="False" />
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
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>One Hour Permission Approved/Rejected Applications</b></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="gvOnehourperm" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="One_Hour_ID" HeaderText="Slno" />
                        
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="OneHour_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Of Apply" HtmlEncode="False" SortExpression="OneHour_Date" />
                        <asp:BoundField DataField="Req_From_Time" HeaderText="From Time" SortExpression="Req_From_Time" />
                        <asp:BoundField DataField="Req_To_Time" HeaderText="To Time" SortExpression="Req_To_Time" />
                        <asp:BoundField DataField="Status3" HeaderText="Status" SortExpression="Status3" />
                    </Columns>
                </asp:GridView>
                
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ONEHOURPERMISSION_SEARCH_SELECT_2" SelectCommandType="StoredProcedure">
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
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }
    </style>
</asp:Content>


