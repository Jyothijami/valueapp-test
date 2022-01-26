<%@ Page Title="" Language="C#" MasterPageFile="~/dev_pages/MobileApp.master" AutoEventWireup="true" CodeFile="LeaveApplication - Copy.aspx.cs" Inherits="dev_pages_LeaveApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .gvStyle {
            text-align: center;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('.numblock').bind('keyup blur', function () {
                $(this).val($(this).val().replace(/[^a-z]/g, ''));
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var date = new Date();
            var currentMonth = date.getMonth();
            var currentDate = date.getDate();
            var currentYear = date.getFullYear();
            $('input[type=date2]').datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'dd/mm/yy',
                minDate: new Date(currentYear, currentMonth, currentDate)
            });
        });
    </script>

    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>


    <div class="col-md-12">

    <table class="pagehead table-responsive "  style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Leave Application</td>
        </tr>
    </table>
         </div>
    <div class="table-responsive">
   
    <div id="divLeaveApp">
        <table >
            <tr style="text-align: left">
                <td>Employee Name :
                </td>
                <td>
                    <asp:TextBox ID="txtEmpName" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>Designation :
                </td>
                <td>
                    <asp:TextBox ID="txtDesignation" ReadOnly="true" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr style="text-align: left">
                <td>Department  :
                </td>
                <td>
                    <asp:TextBox ID="txtDepartment" ReadOnly="true" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="txtFromDate" class="numblock" type="date2" runat="server"></asp:TextBox>
                    *
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
                    <asp:TextBox ID="txtToDate" class="numblock" type="date2" runat="server"></asp:TextBox>

                    *
                    <br />
                    <asp:RadioButtonList ID="rbnTo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbnTo_SelectedIndexChanged">

                        <asp:ListItem Value="0">Session1</asp:ListItem>
                        <asp:ListItem Value="1">Session2</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Avaliable Spl. Leaves :</td>
                <td colspan="4">
                    <asp:Label ID="lblCoff" runat="server" Font-Bold="True" ForeColor="#CC0000"></asp:Label>&nbsp; (To avail Spl. Leaves apply leave as Extra Leave)
                </td>
            </tr>
            <tr style="text-align: left">
                <td>Total Days Of Leave :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTotalDaysOfLeave" ReadOnly="true" OnTextChanged="txtTotalDaysOfLeave_TextChanged" />
                </td>
                <td></td>
                <td>Type Of Leave Applying
                </td>
                <td>
                    <asp:DropDownList ID="ddlTypeOfLeave" AutoPostBack ="true"  runat="server" OnSelectedIndexChanged="ddlTypeOfLeave_SelectedIndexChanged" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">Casual Leave</asp:ListItem>
                        <asp:ListItem Value="2">Earned Leave</asp:ListItem>
                        <asp:ListItem Value="3">Extra Leave</asp:ListItem>
                    </asp:DropDownList>
                    <%--</td>
            </tr>
            <tr style="text-align: left">
                <td>Half/Full Day :
                </td>
                <td colspan="3">--%>
                    <asp:RadioButtonList ID="rbtnFullLeave" runat="server" Visible="false" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnFullLeave_SelectedIndexChanged" AutoPostBack="True">

                        <asp:ListItem Value="0">Half Day</asp:ListItem>
                        <asp:ListItem Value="1">Full Day</asp:ListItem>
                    </asp:RadioButtonList>
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
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btnCalculate" runat="server" Text="Calculate" OnClick="btnCalculate_Click" />
                    <asp:Button Text="Apply Leave" ID="btnApplyLeave" OnClick="btnApplyLeave_Click" Visible="false" runat="server" />
                    <%--<asp:Button ID="btnLeaveHistory" runat="server" Text="Leave History" OnClick="btnLeaveHistory_Click" />--%>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete Leave" Visible="false" OnClick="btnDelete_Click" />

                    <br>
                    <asp:Label ID="lblEmpId" Visible="false" runat="server" />
                    <asp:Label ID="lblEmpName" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblMobile" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblHod_Mbl" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblHOD_Id" runat="server" Visible="False"></asp:Label>

                    <asp:Label ID="lblId" Visible="false" runat="server" />
                    <asp:Label ID="lblMsg" Visible="true" runat="server" />
                    <asp:Label ID="lblLeaveId" Visible="false" runat="server" />
                    <asp:Label ID="lblNoOfLeaves" Visible="false" runat="server" />
                    <asp:Label ID="lblTypeOfLeave" Visible="false" runat="server" />

                    <asp:Label ID="lblLeaveIdTemp" Visible="false" runat="server" />


                    <asp:Label ID="lblExtraLeaves" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lblDeptId" runat="server" Visible="False"></asp:Label>


                </td>
            </tr>
            <tr>
                <td colspan="5">&nbsp;
                </td>
            </tr>
        </table>
    </div>
        </div>
    <div>
        <asp:GridView ID="gvPendingLeaves" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
            <Columns>

                <asp:TemplateField HeaderText="Leave Id" SortExpression="Leave_Id">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnLeaveId" ForeColor="#0066FF" OnClick="lbtnLeaveId_Click" runat="server" Text="<%# Bind('Leave_Id') %>" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Emp Id" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("Emp_Id") %>' />

                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Applied Date" SortExpression="DateOfApplying">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAppliedDate" Text='<%# Eval("DateOfApplying") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="From Date" SortExpression="FromDate">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("FromDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Session" SortExpression="FromSession">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFromSession" Text='<%#Eval("FromSession")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Date" SortExpression="ToDate">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("ToDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Session" SortExpression="ToSession">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblToSession" Text='<%#Eval("ToSession")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No Of Days Applied" SortExpression="AppliedNoOfLeaves">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDaysofLeave" Text='<%# Eval("AppliedNoOfLeaves") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Type of Leave" SortExpression="TypeOfLeave">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblLeaveType" Text='<%#Eval("TypeOfLeave")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reason For Leave" SortExpression="Reason">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblReason" Text='<%# Eval("Reason") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Address In Leave" SortExpression="AddressInLeavePeriod">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAddressInLeave" Text='<%# Eval("AddressInLeavePeriod") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status Of Leave" SortExpression="Status1">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status1") %>' />
                    </ItemTemplate>
                </asp:TemplateField>




            </Columns>
        </asp:GridView>
        <br />
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
            SelectCommand="SELECT Leave_Id, Emp_Id, CONVERT (VARCHAR(10), DateOfApplying, 103) AS DateOfApplying, CONVERT (VARCHAR(10), FromDate, 103) AS FromDate, 
            CONVERT (VARCHAR(10), ToDate, 103) AS ToDate, AppliedNoOfLeaves, TypeOfLeave, Reason, AddressInLeavePeriod, Status1 
            FROM EMP_Leave_tbl WHERE (Status1 = 'Pending')"></asp:SqlDataSource>--%>
    </div>
   <%-- <div>
        <table id="tblRecords" runat="server" visible="false">
            <tr>
                <td class="profilehead">Previous Leave Records :
                </td>
            </tr>
        </table>
    </div>
    <div id="divGV" style="width: 100%">
        <asp:GridView ID="gvLeaveHistory" CssClass="gvStyle" runat="server" Width="100%"></asp:GridView>
    </div>--%>
       
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

