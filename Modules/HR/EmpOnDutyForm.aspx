<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmpOnDutyForm.aspx.cs" Inherits="HR_EmpOnDutyForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderbody" runat="Server">
   
    <table id="TblMain" style="width: 100%">
        <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>On Duty Form Pending Applications</b></td>
        </tr>
        <tr>

            <td colspan="2" align="center">
                <asp:GridView ID="gvOnDutyForm" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AutoGenerateColumns="False" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="OnDuty_ID" SortExpression="OnDuty_ID" HeaderText="Slno" />
                        <asp:TemplateField HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnOnDutyform" runat="server" ForeColor="#0066ff" OnClick="lbtnOnDutyform_Click" Text="<%# Bind('EMP_FIRST_NAME') %>" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="OnDutyDate_From" DataFormatString="{0:dd/MM/yyyy}" HeaderText="OnDuty From" HtmlEncode="False" SortExpression="OnDutyDate_From" />
                        <asp:BoundField DataField="OnDutyDate_To" DataFormatString="{0:dd/MM/yyyy}" HeaderText="OnDuty To" HtmlEncode="False" SortExpression="OnDutyDate_To" />
                        <asp:BoundField DataField="Place_Visited" HeaderText="Place Visited" SortExpression="Place_Visited" />
                        <asp:BoundField DataField="Status1" HeaderText="Status" SortExpression="Status1" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ONDUTY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
                <table id="tblDetails" style="width: 100%" runat="server" visible="false">
                    <tr>
                        <td class="profilehead" colspan="4" style="height: 6px">Employee OnDuty Details</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="Label5" runat="server" Text="Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDate" runat="server" type="datepic" ReadOnly="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="txtDate" ErrorMessage="Please Select Date" ValidationGroup="ds">*</asp:RequiredFieldValidator>

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
                            <asp:TextBox ID="txtLocation" runat="server"></asp:TextBox>

                            <%--<asp:DropDownList ID="ddlLocation" runat="server">
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Moving Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMovingDate" runat="server" type="datepic"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtMovingDate" ErrorMessage="Please Select Moving Date" ValidationGroup="ds">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="OnDuty Date From :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOndutyFrom" runat="server" type="datepic"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtOndutyFrom" ErrorMessage="Please Select OnDuty From Date" ValidationGroup="ds">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Return Date :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtReturndate" runat="server" type="datepic"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtReturndate" ErrorMessage="Please Select Return Date" ValidationGroup="ds">*</asp:RequiredFieldValidator>

                        </td>
                        <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="OnDuty Date To :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtondutyto" runat="server" type="datepic"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtondutyto" ErrorMessage="Please Select OnDuty To Date" ValidationGroup="ds">*</asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server" Text="Moving Time"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMovingTime" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label16" runat="server" Text="Return Time :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtReturnTime" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="Ref Executive"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRefexecutive" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="Nature of Work :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNatureofwork" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="C Off Days :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCoffDays" runat="server"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label15" runat="server" Text="Place Visited :"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPlaceVisited" runat="server"></asp:TextBox>
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
            <td></td>
            <td></td>
        </tr>
    </table>
    <table style="width:100%">
         <tr>
            <td class="searchhead" colspan="2 " style="height: 30px; font-size: 16px"><b>On Duty Form Approved/Rejected Applications</b></td>
        </tr>
        <tr style="width:100%">
            <td colspan="4">
                <asp:GridView ID="gvOnDuty" runat="server" DataSourceID="SqlDataSource2" AllowPaging="True" AutoGenerateColumns="False" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="OnDuty_ID" HeaderText="Slno" />
                        
                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                        <asp:BoundField DataField="OnDutyDate_From" DataFormatString="{0:dd/MM/yyyy}" HeaderText="OnDuty From" HtmlEncode="False" SortExpression="OnDutyDate_From" />
                        <asp:BoundField DataField="OnDutyDate_To" DataFormatString="{0:dd/MM/yyyy}" HeaderText="OnDuty To" HtmlEncode="False" SortExpression="OnDutyDate_To" />
                        <asp:BoundField DataField="Place_Visited" HeaderText="Place Visited" SortExpression="Place_Visited" />
                        <asp:BoundField DataField="Status3" HeaderText="Status" SortExpression="Status3" />
                    </Columns>
                </asp:GridView>
                
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ONDUTY_SEARCH_SELECT_2" SelectCommandType="StoredProcedure">
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

