<%@ Page Title="||Value App : Leave Master : Apply Back Leave ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="ApplyBackLeave.aspx.cs" Inherits="Modules_HR_ApplyBackLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Apply Back Leave</td>
        </tr>
    </table>
    <div id="divLeaveApp">
        <table style="width: 100%">

            <tr style="text-align: left">
                <td>Department  :
                </td>
                <td>
                    <asp:DropDownList ID="ddlDept" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                </td>

                <td style="width: 5%"></td>

                <td>Employee Name :
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmployee" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>

                </td>

            </tr>
            <tr style="text-align: left">
                <td>Designation :
                </td>
                <td>
                    <asp:TextBox ID="txtDesignation" ReadOnly="false" runat="server"></asp:TextBox>

                </td>
                <td style="width: 5%"></td>
                <td>Date Of Applying :
                </td>
                <td>
                    <asp:TextBox ID="txtDateOfApply" ReadOnly="true" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr style="text-align: left">
                <td>Available Casual Leaves :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAvailableCasualLeaves" ReadOnly="true" />
                </td>
                <td style="width: 5%"></td>
                <td>Available Earned Leaves :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAvailableEarnedLeaves" ReadOnly="true" />
                </td>
            </tr>
            <tr style="text-align: left">
                <td>From Date  :
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" type="datepic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtFromDate" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RadioButtonList ID="rbnFrom" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbnFrom_SelectedIndexChanged">

                        <asp:ListItem Value="0">Session1</asp:ListItem>
                        <asp:ListItem Value="1">Session2</asp:ListItem>
                    </asp:RadioButtonList>

                </td>
                <td style="width: 5%"></td>
                <td>To date :
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" type="datepic"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtToDate" ForeColor="Red"></asp:RequiredFieldValidator>

                    <br />
                    <asp:RadioButtonList ID="rbnTo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbnTo_SelectedIndexChanged">

                        <asp:ListItem Value="0">Session1</asp:ListItem>
                        <asp:ListItem Value="1">Session2</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="text-align: left">
                <td>Total Days Of Leave :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTotalDaysOfLeave" ReadOnly="true" />
                </td>
                <td></td>
                <td>Type Of Leave Applying
                    :</td>
                <td>
                    <asp:DropDownList ID="ddlTypeOfLeave" runat="server">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">Casual Leave</asp:ListItem>
                        <asp:ListItem Value="2">Earned Leave</asp:ListItem>
                        <asp:ListItem Value="3">Extra Leave</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="text-align: left">
                <td>Reason For Leave :
                </td>
                <td colspan="4">
                    <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Style="width: 400px" />
                </td>
            </tr>
            <tr style="text-align: left">
                <td>Address In Leave Period :
                </td>
                <td colspan="4">
                    <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" Style="width: 400px" />
                            <asp:TextBox ID="txtAvailableExtra" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>

            <tr style="text-align: left">
                <td>Upload Required Documents :
                </td>
                <td colspan="4">
                    <asp:FileUpload ID="FileUpload1" runat="server" ViewStateMode="Enabled" />
                </td>
                <td>
                    <asp:Label ID="lblImg" runat="server"></asp:Label></td>
            </tr>
            <tr style="text-align: left">
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                    <asp:Button Text="Apply Back Leave" ID="btnApplyLeave" OnClick="btnApplyLeave_Click" Visible="false" runat="server" />

                    <br />
                    <asp:Label ID="lblLeaveId" Visible="false" runat="server" />

                </td>
            </tr>

        </table>
    </div>
    <br />
    <div id="divGV" style="width: 100%">
        <asp:GridView ID="gvPendingLeaves" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%">
            <Columns>
                    <asp:BoundField HeaderText="Leave_Id" DataField="Leave_Id" />

               <%-- <asp:TemplateField HeaderText="Leave Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <%--<ItemTemplate>
                        <asp:LinkButton ID="lbtnLeaveId" runat="server" Text="<%# Bind('Leave_Id') %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Employee Name">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("EMP_FIRST_NAME") %>' />

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Applied Date">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAppliedDate" Text='<%# Eval("DateOfApplying") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="From Date">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("FromDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="From Session" DataField="FromSession" />

                <asp:TemplateField HeaderText="To Date">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("ToDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="To Session" DataField="ToSession" />

                <asp:TemplateField HeaderText="No Of Days Applied">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDaysofLeave" Text='<%# Eval("AppliedNoOfLeaves") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Type of Leave">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblLeaveType" Text='<%#Eval("TypeOfLeave")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reason For Leave">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblReason" Text='<%# Eval("Reason") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Address In Leave">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAddressInLeave" Text='<%# Eval("AddressInLeavePeriod") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status Of Leave">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status1") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
            SelectCommand="SELECT Top 100 EMP_Leave_tbl.Leave_Id, EMP_Leave_tbl.Emp_Id, CONVERT(VARCHAR(10), EMP_Leave_tbl.DateOfApplying, 103) AS DateOfApplying, 
CONVERT(VARCHAR(10), EMP_Leave_tbl.FromDate, 103) AS FromDate, CONVERT(VARCHAR(10), EMP_Leave_tbl.ToDate, 103) AS ToDate, EMP_Leave_tbl.
AppliedNoOfLeaves,TypeOfLeave, EMP_Leave_tbl.Reason, EMP_Leave_tbl.AddressInLeavePeriod, EMP_Leave_tbl.Status1 ,
YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME,FromSession,ToSession
FROM EMP_Leave_tbl,YANTRA_EMPLOYEE_MAST

WHERE (EMP_Leave_tbl.Comment1 = 'Back Leave') and EMP_Leave_tbl.Emp_Id= YANTRA_EMPLOYEE_MAST.EMP_ID

ORDER BY EMP_Leave_tbl.DateOfApplying DESC"></asp:SqlDataSource>
    </div>
</asp:Content>


 
