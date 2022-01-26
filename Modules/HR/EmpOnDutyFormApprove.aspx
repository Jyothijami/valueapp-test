<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmpOnDutyFormApprove.aspx.cs" Inherits="Modules_HR_EmpOnDutyFormApprove" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        // Let's use a lowercase function name to keep with JavaScript conventions
        function selectAll(invoker) {
            // Since ASP.NET checkboxes are really HTML input elements
            //  let's get all the inputs 
            var inputElements = document.getElementsByTagName('input');

            for (var i = 0 ; i < inputElements.length ; i++) {
                var myElement = inputElements[i];

                // Filter through the input types looking for checkboxes
                if (myElement.type === "checkbox") {

                    // Use the invoker (our calling element) as the reference 
                    //  for our checkbox status
                    myElement.checked = invoker.checked;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
                <tr>
                    <td style="text-align: left;">HR Approval Pages :</td>

                    <td style="text-align: right"></td>
                </tr>
            </table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center; font-size: medium;">
                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="True" NavigateUrl="~/Modules/HR/HRApproval.aspx" Target="_blank">Leave Applications</asp:HyperLink>
                        &nbsp;||&nbsp;
                        <asp:LinkButton ID="lnkOnDuty" runat="server" OnClick="lnkOnDuty_Click" Font-Underline="True">On Duty Forms</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkOneHour" runat="server" OnClick="lnkOneHour_Click" Font-Underline="True">One Hour Permissions</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkOverTime" runat="server" OnClick="lnkOverTime_Click" Font-Underline="True">Over Time</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkShiftChange" runat="server" OnClick="lnkShiftChange_Click" Font-Underline="True">Shift Change</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkTickets" runat="server" OnClick="lnkTickets_Click" Font-Underline="True">Ticket Details</asp:LinkButton>

                    </td>
                </tr>
            </table>
            <br />
            <asp:Panel runat="server" ID="OnDutyForm" Visible="false">
                <h4>Pending OnDuty Applications</h4>

                <div id="divOnDuty">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvOndutyPend" runat="server" DataSourceID="SqlDataSource1" AllowPaging="True" AutoGenerateColumns="False" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True" OnRowDataBound="gvOndutyPend_RowDataBound" >
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Chk" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SI.No" SortExpression="OnDuty_ID" Visible="true">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("OnDuty_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                                        
                                        <asp:BoundField DataField="OnDutyDate_From" DataFormatString="{0:dd/MM/yyyy}" HeaderText="OnDuty From" HtmlEncode="False" SortExpression="OnDutyDate_From" />
                                        <asp:BoundField DataField="FromTime" HeaderText="From Time" SortExpression="FromTime" />
                                        <asp:BoundField DataField="OnDutyDate_To" DataFormatString="{0:dd/MM/yyyy}" HeaderText="OnDuty To" HtmlEncode="False" SortExpression="OnDutyDate_To" />
                                        <asp:BoundField DataField="ToTime" HeaderText="To Time" SortExpression="ToTime" />
                                        <asp:BoundField DataField="Place_Visited" HeaderText="Place Visited" SortExpression="Place_Visited" />
                                        <asp:BoundField DataField="Nature_Of_Work" HeaderText="Purpose" SortExpression="Purpose">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status2" HeaderText="Status" SortExpression="Status2" />
                                        <asp:BoundField DataField="Emp_Id" HeaderText="EmpId" SortExpression="Emp_Id" />
                                        <asp:TemplateField HeaderText="Spl. Leave" Visible="true">
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCompOff" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <SelectedRowStyle BackColor="Silver" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ONDUTY_Approve_SEARCH_SELECT_HR" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnOnDutyAppr" runat="server" Text="Approve" OnClick="btnOnDutyAppr_Click" />

                                &nbsp;<asp:Button ID="btnOnDutyReject" runat="server" Text="Reject" OnClick="btnOnDutyReject_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <table>

                    <tr>
                        <td>Employee Name :<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>From Date :<asp:TextBox ID="txtFromDate" type="date" runat="server"></asp:TextBox></td>
                        <td>To Date :<asp:TextBox ID="txtToDate" type="date" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divGV">
                    <asp:GridView ID="gvOnDutyHist" AllowPaging="True" OnPageIndexChanging="gvOnDutyHist_PageIndexChanging" CssClass="gvStyle" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" >
                        <Columns>

                            <asp:BoundField DataField="OnDuty_ID" SortExpression="OnDuty_ID" HeaderText="SI.No" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                            <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                            <asp:BoundField DataField="OnDutyDate_From" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date" HtmlEncode="False" SortExpression="OnDutyDate_From" />
                            <asp:BoundField DataField="OnDutyDate_To" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date" HtmlEncode="False" SortExpression="OnDutyDate_To" />

                            <asp:BoundField DataField="FromTime" HeaderText="From Time" SortExpression="FromTime" />
                            <asp:BoundField DataField="ToTime" HeaderText="To Time" SortExpression="ToTime" />

                            <asp:BoundField DataField="Place_Visited" HeaderText="Place Visited" SortExpression="Place_Visited" />
                            <asp:BoundField DataField="Nature_Of_Work" HeaderText="Purpose" SortExpression="Purpose">
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="C_Off_Days" HeaderText="C-Off Days" SortExpression="C_Off_Days" />

                            <asp:BoundField DataField="Status3" HeaderText="Final Status" SortExpression="Status3" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ONDUTY_Approve_SEARCH_SELECT_HR_2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                            <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="OneHourPerm" Visible="false">
                <h4>Pending OneHour Applications</h4>
                <div id="divOneHourPend">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvOneHrPend" runat="server" DataSourceID="SqlDataSource3" AllowPaging="True" AutoGenerateColumns="False" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Chk" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id" SortExpression="One_Hour_ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblOnehour" Text='<%#Eval("One_Hour_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />

                                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                                        <asp:BoundField DataField="OneHour_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Of Apply" HtmlEncode="False" SortExpression="OneHour_Date" />
                                        <asp:BoundField DataField="Req_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Required Date" HtmlEncode="False" SortExpression="Req_Date" />
                                        <asp:BoundField DataField="Req_From_Time" HeaderText="From Time" SortExpression="Req_From_Time" />
                                        <asp:BoundField DataField="Req_To_Time" HeaderText="To Time" SortExpression="Req_To_Time" />
                                        <asp:BoundField DataField="Status2" HeaderText="Status" SortExpression="Status2" />

                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_OneHour_Approve_SEARCH_SELECT_HR" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />

                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnOneHrAppr" runat="server" Text="Approve" OnClick="btnOneHrAppr_Click" />

                                &nbsp;<asp:Button ID="btnOneHrReject" runat="server" Text="Reject" OnClick="btnOneHrReject_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divOneHr">
                    <asp:GridView ID="gvOneHrhist" DataSourceID="SqlDataSource4" AllowPaging="True" CssClass="gvStyle" runat="server" Width="100%" AutoGenerateColumns="False">
                        <Columns>

                            <asp:BoundField DataField="One_Hour_ID" HeaderText="Id" SortExpression="One_Hour_ID" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                            <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                            <asp:BoundField DataField="OneHour_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="OneHour_Date" />
                            <asp:BoundField DataField="Req_From_Time" HeaderText="From Time" SortExpression="Req_From_Time" />
                            <asp:BoundField DataField="Req_To_Time" HeaderText="To Time" SortExpression="Req_To_Time" />
                            <asp:BoundField DataField="Status3" HeaderText="Final Status" SortExpression="Status3" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_OneHour_Approve_SEARCH_SELECT_HR_2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                            <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />

                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>

            </asp:Panel>
            <asp:Panel runat="server" ID="OverTime" Visible="false">
                <h4>Pending Over Time Applications</h4>
                <div id="divOverTime">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvOvertime" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True" OnRowDataBound="gvOvertime_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Chk" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id" SortExpression="Overtime_ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblOverTime" Text='<%#Eval("Overtime_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME"  />

                                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                                        <asp:BoundField DataField="Overtime_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="Overtime_Date" />
                                        <asp:BoundField DataField="Worked_From_Time" HeaderText="From Time" DataFormatString="{0:HH/mm}" SortExpression="Worked_From_Time" />
                                        <asp:BoundField DataField="Worked_Upto_Time" HeaderText="To Time" DataFormatString="{0:HH/mm}" SortExpression="Worked_Upto_Time" />
                                        <asp:BoundField DataField="Nature_Of_Work" HeaderText="Nature of Work" SortExpression="Nature_Of_Work" />
                                        <asp:BoundField DataField="Status2" HeaderText="Status" SortExpression="Status2" />
                                         <asp:BoundField DataField="Emp_Id" HeaderText="EmpId" SortExpression="Emp_Id"/>
                                        <asp:TemplateField HeaderText="Spl. Leave" Visible="true">
                                            <HeaderStyle HorizontalAlign="Center"/>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCompOff" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_OverTime_SEARCH_SELECT_HR" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnOverTimeAppr" runat="server" Text="Approve" OnClick="btnOverTimeAppr_Click" />

                                &nbsp;<asp:Button ID="btnOverTimeReject" runat="server" Text="Reject" OnClick="btnOverTimeReject_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <table>

                    <tr>
                        <td>Employee Name :<asp:TextBox ID="txtEmpName_Overtime" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>From Date :<asp:TextBox ID="txtFromDate_Overtime" type="date" runat="server"></asp:TextBox></td>
                        <td>To Date :<asp:TextBox ID="txtToDate_Overtime" type="date" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSearch_Overtime" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Overtime_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divOvrTime">
                    <asp:GridView ID="gvOvertimeHist" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="gvOvertimeHist_PageIndexChanging" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                        <Columns>
                            <asp:BoundField DataField="Overtime_ID" HeaderText="Slno" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                            <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                            <asp:BoundField DataField="Overtime_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="Overtime_Date" />
                            <asp:BoundField DataField="Worked_From_Time" HeaderText="From Time" DataFormatString="{0:HH/mm}" SortExpression="Worked_From_Time" />
                            <asp:BoundField DataField="Worked_Upto_Time" HeaderText="To Time" DataFormatString="{0:HH/mm}" SortExpression="Worked_Upto_Time" />
                            <asp:BoundField DataField="Nature_Of_Work" HeaderText="Nature of Work" SortExpression="Nature_Of_Work" />
                            <asp:BoundField DataField="C_Off_Days" HeaderText="C-Off Days" SortExpression="C_Off_Days" />
                            <asp:BoundField DataField="Status3" HeaderText="Final Status" SortExpression="Status3" />

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_OverTime_SEARCH_SELECT_HR_2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                            <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                            <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="ShiftChange" Visible="false">
                <h4>Pending Shift Change Applications</h4>
                <div id="divShiftChange">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvShiftchange" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource7" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Chk" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id" SortExpression="Shift_Change_ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblShiftChange" Text='<%#Eval("Shift_Change_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                                        <asp:BoundField DataField="Shift_Change_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="Shift_Change_Date" />
                                        <asp:BoundField DataField="Present_Shift" HeaderText="Present Shift" SortExpression="Present_Shift" />
                                        <asp:BoundField DataField="Required_Shift" HeaderText="Required Shift" SortExpression="Required_Shift" />
                                        <asp:BoundField DataField="Status2" HeaderText="Status" SortExpression="Status2" />
                                    </Columns>
                                </asp:GridView>

                                <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ShiftChange_SEARCH_SELECT_HR" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnShiftAppr" runat="server" Text="Approve" OnClick="btnShiftAppr_Click" />

                                &nbsp;<asp:Button ID="btnShiftReject" runat="server" Text="Reject" OnClick="btnShiftReject_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divShiftChange2">
                    <asp:GridView ID="gvShiftchangeHist" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource8" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">
                        <Columns>
                            <asp:BoundField DataField="Shift_Change_ID" HeaderText="SI.No" SortExpression="Shift_Change_ID" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                            <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="DEPT_NAME" />
                            <asp:BoundField DataField="Shift_Change_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" SortExpression="Shift_Change_Date" />
                            <asp:BoundField DataField="Present_Shift" HeaderText="Present Shift" SortExpression="Present_Shift" />
                            <asp:BoundField DataField="Required_Shift" HeaderText="Required Shift" SortExpression="Required_Shift" />
                            <asp:BoundField DataField="Status3" HeaderText="Final Status" SortExpression="Status3" />

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_ShiftChange_SEARCH_SELECT_HR_2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                            <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                            <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="TicketDetails" Visible="false">
                <h4>Pending Ticket Detail Applications</h4>
                <div id="divTicket">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:GridView ID="gvTicketdetails" runat="server" DataSourceID="SqlDataSource9" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="EMP_ID1,DEPT_ID1,DESG_ID1" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">

                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnClick="selectAll(this)" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="Chk" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Id" SortExpression="TicketDetails_Id">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTicketDetails" Text='<%#Eval("TicketDetails_Id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                                        <asp:BoundField DataField="Mobile_Number" HeaderText="Emp Mobile" SortExpression="Mobile_Number" />
                                        <asp:BoundField DataField="Age" HeaderText="Emp Age" SortExpression="Age" />
                                        <asp:BoundField DataField="Moving_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Moving_Date" HtmlEncode="False" SortExpression="Moving_Date" />
                                        <asp:BoundField DataField="Mode_Travel" HeaderText="Mode_Travel" SortExpression="Mode_Travel" />
                                        <asp:BoundField DataField="Location_ID" HeaderText="Starting Point" SortExpression="Location_ID" />

                                        <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                                        <asp:BoundField DataField="Comment1" HeaderText="Purpose" SortExpression="Purpose">
                                        <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status2" HeaderText="Status" SortExpression="Status2" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="Silver" />
                                </asp:GridView>

                                <asp:SqlDataSource ID="SqlDataSource9" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_TICKETDETAILS_SEARCH_SELECT_HR" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                                        <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                                        <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>

                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnTicketAppr" runat="server" Text="Approve" OnClick="btnTicketAppr_Click" />

                                &nbsp;<asp:Button ID="btnTicketReject" runat="server" Text="Reject" OnClick="btnTicketReject_Click" />

                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divTicket2">
                    <asp:GridView ID="gvTicketdetailsHist" runat="server" DataSourceID="SqlDataSource10" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="EMP_ID1,DEPT_ID1,DESG_ID1" Width="100%" SelectedRowStyle-BackColor="#c0c0c0" AllowSorting="True">

                        <Columns>
                            <asp:BoundField DataField="TicketDetails_Id" HeaderText="Slno" />
                            <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Employee Name" SortExpression="EMP_FIRST_NAME" />
                            <asp:BoundField DataField="Mobile_Number" HeaderText="Emp Mobile" SortExpression="Mobile_Number" />
                            <asp:BoundField DataField="Age" HeaderText="Emp Age" SortExpression="Age" Visible="False" />
                            <asp:BoundField DataField="Moving_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Moving_Date" HtmlEncode="False" SortExpression="Moving_Date" />
                            <asp:BoundField DataField="Mode_Travel" HeaderText="Mode_Travel" SortExpression="Mode_Travel" />
                            <asp:BoundField DataField="Location_ID" HeaderText="Starting Point" SortExpression="Location_ID" />
                            <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                            <asp:BoundField DataField="Comment1" HeaderText="Purpose" SortExpression="Purpose">
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status3" HeaderText="Final Status" SortExpression="Status3" />
                        </Columns>
                        <SelectedRowStyle BackColor="Silver" />
                    </asp:GridView>

                    <asp:SqlDataSource ID="SqlDataSource10" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_HR_TICKETDETAILS_SEARCH_SELECT_HR_2" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="SearchItemName" Type="String" />
                            <asp:Parameter DefaultValue="0" Name="SearchValue" Type="String" />
                            <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="userType" PropertyName="Text" Type="Int64" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </asp:Panel>

            <asp:Label ID="lblUserType" runat="server" Visible="False"></asp:Label>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

