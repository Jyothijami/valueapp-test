<%@ Page Title="||Value App : Leave Master : Leave History ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="LeaveHistory.aspx.cs" Inherits="Modules_HR_LeaveHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
   
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
    <table style="width: 100%">
                <tr>
                    <td style="text-align: center; font-size: medium;">
                        
                       
                            <asp:LinkButton ID="lnkOneHour" runat="server" OnClick="lnkOneHour_Click" Font-Underline="True">One Hour Permissions History</asp:LinkButton>
                        &nbsp;||&nbsp;
                            <asp:LinkButton ID="lnkTickets" runat="server" OnClick="lnkTickets_Click" Font-Underline="True">Ticket Details History</asp:LinkButton>

                    </td>
                </tr>
            </table>
    <asp:Panel ID="pnlLeaveHistory" runat ="server" >
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Complete Leave History</td>
            <td style="text-align: right">
                        <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%">
        <tr>
            <td style="text-align: right">Employee Name :</td>
            <td>
                <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                <asp:DropDownList id="ddlEmpName" runat="server"></asp:DropDownList>
            </td>
            <td style="text-align: right">Status :</td>
            <td>
                <%--<asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>--%>
                <asp:DropDownList id="ddlStatus" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="0" >--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">From Date :</td>
            <td>
                <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox></td>
            <td style="text-align: right">To Date :</td>
            <td>
                <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">Type Of Leave:</td>
            <td>
                <asp:DropDownList id="ddlType" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="0" >--</asp:ListItem>
                </asp:DropDownList>
                <%--<asp:TextBox ID="txtType" runat="server"></asp:TextBox>--%></td>
            <td style="text-align: right" colspan="2"></td>
            
        </tr>
        
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExprot" runat="server" Text="Export To Excel" OnClick="btnExprot_Click"  EnableTheming="True"   />
                <asp:Button ID="btnRunReport" runat ="server" Text="Run Report" OnClick ="btnRunReport_Click" />
                <asp:Button ID="btnRunReportEmp" runat ="server" Text="Run Report Emp" OnClick ="btnRunReportEmp_Click" />

            </td>
        </tr>
        <tr>
            <td>
                Total No of Leaves Applied :&nbsp;&nbsp; <asp:Label ID="lblTotalLeaveCount" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
            <td>
                
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <br />
    <div id="divGV">
        <asp:GridView ID="gvLeaveHistory" CssClass="gvStyle" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found" Width="100%" AllowPaging="True" OnPageIndexChanging="gvLeaveHistory_PageIndexChanging" OnRowDataBound="gvLeaveHistory_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkhdr" runat="server" OnClick="selectAll(this)" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="Chk" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Leave_Id" HeaderText="Leave Id" SortExpression="Leave_Id" />
                <asp:BoundField DataField="Emp_Id" HeaderText="Emp Id" SortExpression="Emp_Id" />
                <asp:BoundField DataField="Employee name" HeaderText="Employee name" SortExpression="Employee name" />
                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="Applied Date" HeaderText="Applied Date" ReadOnly="True" SortExpression="Applied Date" />
                <asp:BoundField DataField="FromDate" HeaderText="FromDate" ReadOnly="True" SortExpression="FromDate" />
                <asp:BoundField HeaderText="From Session" DataField="FromSession" />
                <asp:BoundField DataField="ToDate" HeaderText="ToDate" ReadOnly="True" SortExpression="ToDate" />
                <asp:BoundField HeaderText="To Session" DataField="ToSession" />
                <asp:BoundField DataField="No Of Days" HeaderText="No Of Days" SortExpression="No Of Days" />
                <asp:BoundField DataField="Type Of Leave" HeaderText="Type Of Leave" SortExpression="Type Of Leave" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
                <asp:BoundField DataField="Address In Leave" HeaderText="Address In Leave" SortExpression="Address In Leave" />
                <asp:BoundField DataField="Leave Status" HeaderText="Status" SortExpression="Leave Status" />
                <asp:BoundField DataField="Leave State" HeaderText="Leave State" SortExpression="Leave State" />
                <asp:BoundField DataField="Rejected By" HeaderText="Rejected By" SortExpression="Rejected By" />
                
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT Emp_Name AS [Employee name], Emp_Designation AS Designation, Emp_Department AS Department, CONVERT (VARCHAR(10), DateOfApplying, 103) AS [Applied Date], AppliedNoOfLeaves AS [No Of Days], TypeOfLeave AS [Type Of Leave], Reason, AddressInLeavePeriod AS [Address In Leave], Status3 AS [Leave Status], Rejected_By AS [Rejected By], Comment1 AS [Leave State] FROM EMP_Leave_tbl WHERE (Status3 = 'Pending') OR (Status3 = 'Approved') OR (Status3 = 'Rejected') ORDER BY DateOfApplying DESC"></asp:SqlDataSource>
    </div>
    <br />
    <asp:Label ID="lblEmpId" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblNoOfLeaves" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblTypeOfLeave" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblLeaveIdTemp" runat="server" Visible="false"></asp:Label>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel Leave" OnClick="btnCancel_Click" />
        </asp:Panel>
    <asp:Panel ID="pnlOneHour" runat ="server" Visible ="false" >
        <div id="divOneHourPend">
            <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">OneHour Permission History</td>
            <td style="text-align: right">
                        <asp:DropDownList ID="ddlHistoryNoOfRecords" runat="server" OnSelectedIndexChanged="ddlHistoryNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
        </tr>
    </table>
            <br />
            <table style="width: 100%">
        <tr>
            <td style="text-align: right">Employee Name :</td>
            <td>
                <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                <asp:DropDownList id="ddlOneHourEmpName" runat="server"></asp:DropDownList>
            </td>
            <td style="text-align: right">Status :</td>
            <td>
                <%--<asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>--%>
                <asp:DropDownList id="ddlOneHourStatus" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="0" >--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">From Date :</td>
            <td>
                <asp:TextBox ID="txtOneHrFrmDt" type="datepic" runat="server"></asp:TextBox></td>
            <td style="text-align: right">To Date :</td>
            <td>
                <asp:TextBox ID="txtOneHrToDt" type="datepic" runat="server"></asp:TextBox></td>
        </tr>
        
        
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnOneHrSearch" runat="server" Text="Search" OnClick="btnOneHrSearch_Click" />
                <asp:Button ID="btnOneHrExport" runat="server" Text="Export To Excel" OnClick="btnExprot_Click"  EnableTheming="True"   />
            </td>
        </tr>
        <tr>
            <td>
                Total No of One Hour Permission Applied :&nbsp;&nbsp; <asp:Label ID="Label1" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>
            <td>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
           <asp:GridView ID="gvOneHourHistory" CssClass="gvStyle" runat="server" AutoGenerateColumns="False" OnPageIndexChanging ="gvOneHourHistory_PageIndexChanging">
               <Columns >
                    <asp:BoundField DataField="One_Hour_ID" HeaderText="One_Hour_ID " SortExpression="One_Hour_ID" />
                <%--<asp:BoundField DataField="Emp_Id" HeaderText="Emp Id" SortExpression="Emp_Id" />--%>
                <asp:BoundField DataField="Empname" HeaderText="Employee name" SortExpression="Employee name" />
                <asp:BoundField DataField="DESG_NAME" HeaderText="Designation" SortExpression="Designation" />
                <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="AppliedDt" HeaderText="Applied Date" ReadOnly="True" SortExpression="AppliedDt" />
                <%--<asp:BoundField DataField="FromDate" HeaderText="FromDate" ReadOnly="True" SortExpression="FromDate" />
                <asp:BoundField HeaderText="From Session" DataField="FromSession" />
                <asp:BoundField DataField="ToDate" HeaderText="ToDate" ReadOnly="True" SortExpression="ToDate" />
                <asp:BoundField HeaderText="To Session" DataField="ToSession" />--%>
                <asp:BoundField DataField="ReqDt" HeaderText="ReqDt" SortExpression="ReqDt" />
                <%--<asp:BoundField DataField="Type Of Leave" HeaderText="Type Of Leave" SortExpression="Type Of Leave" />--%>
                <asp:BoundField DataField="Reason_For_Permission" HeaderText="Reason" SortExpression="Reason" />
                <%--<asp:BoundField DataField="Address In Leave" HeaderText="Address In Leave" SortExpression="Address In Leave" />--%>
                <asp:BoundField DataField="Status3" HeaderText="Status" SortExpression="Leave Status" />
                <%--<asp:BoundField DataField="Leave State" HeaderText="Leave State" SortExpression="Leave State" />--%>
                <asp:BoundField DataField="Rejected_By" HeaderText="Rejected By" SortExpression="Rejected By" />
               </Columns>
           </asp:GridView>

           
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTicket" runat ="server" Visible ="false" >
        <div id="divTicket">
            <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Ticket Application History</td>
            <td style="text-align: right">
                        <asp:DropDownList ID="ddlTicket" runat="server" OnSelectedIndexChanged="ddlTicket_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>25</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>75</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                    </td>
        </tr>
    </table>
            <br />
            <table style="width: 100%">
        <tr>
            <td style="text-align: right">Employee Name :</td>
            <td>
                <%--<asp:TextBox ID="txtName" runat="server"></asp:TextBox>--%>
                <asp:DropDownList id="ddlTicketEmpName" runat="server"></asp:DropDownList>
            </td>
            <td style="text-align: right">Status :</td>
            <td>
                <%--<asp:TextBox ID="txtStatus" runat="server"></asp:TextBox>--%>
                <asp:DropDownList id="ddlTicketStatus" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="0" >--</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="text-align: right">From Date :</td>
            <td>
                <asp:TextBox ID="txtTicketFromDt" type="datepic" runat="server"></asp:TextBox></td>
            <td style="text-align: right">To Date :</td>
            <td>
                <asp:TextBox ID="txtTicketToDt" type="datepic" runat="server"></asp:TextBox></td>
        </tr>
        
        
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnTicketSearch" runat="server" Text="Search" OnClick="btnTicketSearch_Click" />
                <asp:Button ID="Button2" runat="server" Text="Export To Excel" OnClick="btnExprot_Click"  EnableTheming="True"   />
            </td>
        </tr>
        <tr>
            <%--<td>
                Total No of One Hour Permission Applied :&nbsp;&nbsp; <asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;
            </td>--%>
            <td>
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
           <asp:GridView ID="gvTicketHistory" CssClass="gvStyle" runat="server" AutoGenerateColumns="False" OnPageIndexChanging ="gvTicketHistory_PageIndexChanging">
               <Columns >
                    <asp:BoundField DataField="TicketDetails_Id" HeaderText="Ticket Id " SortExpression="TicketDetails_Id" />
                <%--<asp:BoundField DataField="Emp_Id" HeaderText="Emp Id" SortExpression="Emp_Id" />--%>
                <asp:BoundField DataField="Empname" HeaderText="Employee name" SortExpression="Employee name" />
                <asp:BoundField DataField="DESG_NAME" HeaderText="Designation" SortExpression="Designation" />
                <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="AppliedDt" HeaderText="Applied Date" ReadOnly="True" SortExpression="AppliedDt" />
                <asp:BoundField DataField="Age" HeaderText="Age" ReadOnly="True" SortExpression="Age" />
                <asp:BoundField HeaderText="Mobile_Number" DataField="Mobile_Number" />
                <asp:BoundField DataField="Location_ID" HeaderText="Location_ID" ReadOnly="True" SortExpression="Location_ID" />
                <asp:BoundField HeaderText="Mode_Travel" DataField="Mode_Travel" />
                <asp:BoundField DataField="ReqDt" HeaderText="ReqDt" SortExpression="ReqDt" />

                <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                <asp:BoundField DataField="Eligibility" HeaderText="Eligibility" SortExpression="Eligibility" />
                <asp:BoundField DataField="IdProof_Number" HeaderText="IdProof_Number" SortExpression="IdProof_Number" />
                <asp:BoundField DataField="Comment1" HeaderText="Comment1" SortExpression="Comment1" />
                <asp:BoundField DataField="Status3" HeaderText="Status" SortExpression="Status3" />
                <asp:BoundField DataField="Rejected_By" HeaderText="Rejected By" SortExpression="Rejected By" />
               </Columns>
           </asp:GridView>

           
        </div>
    </asp:Panel>
</asp:Content>

