<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Emp_AttendenceNew.aspx.cs" Inherits="Modules_HR_Emp_AttendenceNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="stacktable">
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">Bio-Metric Attendance Details</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" Visible="true" OnRowDataBound="GridView1_RowDataBound" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="8">
                    <Columns>
                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" SortExpression="Emp_Name" />
                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                        <asp:BoundField DataField="Att_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date of Attendance" HtmlEncode="False" SortExpression="Att_Date" />
                        <asp:BoundField DataField="Intime" HeaderText="In Time" SortExpression="Intime" />
                        <%--<asp:BoundField DataField="InRemark" HeaderText="InRemark" SortExpression="InRemark" />--%>
                        <asp:BoundField DataField="Outtime" HeaderText="Out Time" SortExpression="Outtime" />
                        <%--<asp:BoundField DataField="OutRemark" HeaderText="Out Remark" SortExpression="OutRemark" />--%>
                        <asp:BoundField DataField="Totaltime" HeaderText="Total Time" SortExpression="Totaltime" />
                        <asp:BoundField DataField="CP_SHORT_NAME" HeaderText="Company Name" SortExpression="CP_FULL_NAME" />
                    </Columns>
                </asp:GridView>

                <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT top 60  YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME, Emp_Attendance.Department, Emp_Attendance.Att_Date, Emp_Attendance.Intime, Emp_Attendance.Outtime, Emp_Attendance.Totaltime, Emp_Attendance.OutRemark, Emp_Attendance.InRemark, YANTRA_COMP_PROFILE.CP_FULL_NAME FROM EMP_BIO_MAP INNER JOIN Emp_Attendance ON EMP_BIO_MAP.emp_code = Emp_Attendance.Emp_Code INNER JOIN YANTRA_EMPLOYEE_MAST ON EMP_BIO_MAP.app_emp_id = YANTRA_EMPLOYEE_MAST.EMP_ID INNER JOIN YANTRA_COMP_PROFILE ON YANTRA_EMPLOYEE_MAST.CP_ID = YANTRA_COMP_PROFILE.CP_ID"></asp:SqlDataSource>--%>
            </td>
        </tr>
    </table>
</asp:Content>

