<%@ Page Title="||Value App : Leave Master : Leave Allocation ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="LeaveAllocation.aspx.cs" Inherits="Modules_HR_LeaveAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <%--Script Code Starts--%>

    <script>
        $(document).ready(function () {
            $('#<%=lblNoRecords.ClientID%>').css('display', 'none');

            $(document).on('keypress', '#<%=txtSearch.ClientID%>', function (e) {
                // Hide No records to display label.
                $('#<%=lblNoRecords.ClientID%>').css('display', 'none');
                //Hide all the rows.
                $("#<%=gvUpdateLeaves.ClientID%> tr:has(td)").hide();

                var iCounter = 0;
                //Get the search box value
                var sSearchTerm = $('#<%=txtSearch.ClientID%>').val();

                //if nothing is entered then show all the rows.
                if (sSearchTerm.length == 0) {
                    $("#<%=gvUpdateLeaves.ClientID%> tr:has(td)").show();
                    //return false;
                }
                //Iterate through all the td.
                $("#<%=gvUpdateLeaves.ClientID%> tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    //Check if data matches
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) {
                        $(this).parent().show();
                        iCounter++;
                        //return true;
                    }
                });
                if (iCounter == 0) {
                    $('#<%=lblNoRecords.ClientID%>').css('display', '');
                }
            });
        });
    </script>

    <%-- Script Code Ends--%>
       <%-- <table class="stacktable">
                <tr>
                    <td>
                        <%--<asp:Button ID="btnLeaveAllocation" runat="server" Text="Leave Allocation" Width="130px" PostBackUrl="~/Modules/HR/LeaveAllocation.aspx" />--%>
                        
                    <%--    <asp:Button ID="btnLeaveHistory" runat="server" Text="Leave History" Width="130px" PostBackUrl="~/Modules/HR/LeaveHistory.aspx" />
                        
                        <asp:Button ID="btnHodApproval" runat="server" Text="HOD Approval" Width="130px" PostBackUrl="~/Modules/HR/HODApproval.aspx" />

                        <asp:Button ID="btnHrApproval" runat="server" Text="HR Approval" Width="130px" PostBackUrl="~/Modules/HR/HRApproval.aspx" />

                        <asp:Button ID="btnMdApproval" runat="server" Text="MD Approval" Width="130px" PostBackUrl="~/Modules/HR/MDApproval.aspx" />

                        <asp:Button ID="btnApplyBackLeave" runat="server" Text="Apply Back Leave" Width="130px" PostBackUrl="~/Modules/HR/ApplyBackLeave.aspx" />
                        
                        <asp:Button ID="btnCalender" runat="server" Text="Holiday Calender" Width="130px" PostBackUrl="~/Modules/HR/Calender.aspx" />
                    </td>

                </tr>
            </table>--%>
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Leave Allocation</td>
        </tr>
    </table>
    <div>
        <table>

            <tr style="text-align: left">
                <td>No Of Casual Leaves :
                </td>
                <td>
                    <asp:TextBox ID="txtCasualLeaves" runat="server" />
                </td>
                <td style="width: 5%"></td>
                <td>No Of Earned Leaves :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEarnedLeaves" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Update Leaves" OnClick="btnSave_Click" />

                </td>
            </tr>
            <%--<tr>
                <td colspan="6" style="text-align: center">
                    <asp:Label ID="lblMsg" runat="server"  />
                </td>
            </tr>--%>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>Search Text :<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                </td>
                <td style="width: 5%"></td>
                <td>
                    <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Search" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblNoRecords" runat="server" Text="No records to display"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:GridView ID="gvUpdateLeaves" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="Chk" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Leave Id" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpId" Text='<%#Eval("EMP_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Employee Name">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpname" Text='<%#Eval("EMP_FIRST_NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Desg Id" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDesgId" Text='<%#Eval("DESG_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Designation">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDesignation" Text='<%# Eval("DESG_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Dept Id" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDepartmentId" Text='<%#Eval("DEPT_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Department ">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDepartment" Text='<%#Eval("DEPT_NAME")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Casual Leaves">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCasualLeaves" Text='<%# Eval("Casual_Leaves") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Earned Leaves">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEarnedLeaves" Text='<%#Eval("Earned_Leaves")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>
        </asp:GridView>

        <br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
            SelectCommand="SELECT a.EMP_ID, a.EMP_FIRST_NAME , c.DESG_ID, c.DESG_NAME , d.DEPT_ID, d.DEPT_NAME,b.EMP_DET_DOT,e.Casual_Leaves, e.Earned_Leaves FROM YANTRA_EMPLOYEE_MAST AS a INNER JOIN YANTRA_EMPLOYEE_DET AS b ON a.EMP_ID = b.EMP_ID INNER JOIN YANTRA_DESG_MAST AS c ON c.DESG_ID = b.DESG_ID INNER JOIN YANTRA_DEPT_MAST AS d ON d.DEPT_ID = b.DEPT_ID LEFT OUTER JOIN Leave_tbl AS e ON e.EMP_Id = a.EMP_ID where  b.EMP_DET_DOT >= GETDATE() and a.STATUS !=0  and a.STATUS is not null order by a.EMP_FIRST_NAME "></asp:SqlDataSource>

    </div>
</asp:Content>

