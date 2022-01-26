<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Emp_Attendance - Copy.aspx.cs" Inherits="Modules_HR_Emp_Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table >
        <tr>
            <td>
                <asp:DropDownList ID="ddlReport" runat ="server" AutoPostBack ="true" OnSelectedIndexChanged ="ddlReport_SelectedIndexChanged" >
                    <asp:ListItem >--</asp:ListItem>

                    <asp:ListItem >Total Time</asp:ListItem>
                    <asp:ListItem >In Time</asp:ListItem>
                    <asp:ListItem >Out Time</asp:ListItem>

                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Export To Excel" OnClick="Button2_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White" Width="30%" />

            </td>
        </tr>
        <tr>
            <td colspan="4">

                <asp:GridView ID="gdvAttendence" Visible ="false"  runat="server" Width="100%">
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table class="stacktable">
        <tr>
            <td colspan="4" style="text-align: left" class="profilehead">Bio-Metric Attendance Details</td>
        </tr>
        <tr>
            <%--<td style="/*text-align: right*/">Location :</td>--%>
            <td colspan="3" style="text-align: left">
                <asp:DropDownList Visible ="false"  ID="ddlLocation" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="locname" DataValueField="locid">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [locid], [locname] FROM [location_tbl]"></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <%--<td style="text-align: right; font-weight: bold">Upload Biometric Excel :</td>--%>
            <td style="text-align: left" colspan="2">
                <asp:FileUpload Visible ="false"  ID="FileUpload1" runat="server" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td colspan="3" style="text-align:left">
                <asp:Button ID="Button1" Visible ="false"  runat="server" OnClick="Button1_Click" Text="Upload" />

            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td>Emp Name :
                <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
            </td>
            <td>From Dt :
                <asp:TextBox ID="txtFromDate" type="datepic" runat="server"></asp:TextBox>
            </td>
            <td>To Dt :
                <asp:TextBox ID="txtToDate" type="datepic" runat="server"></asp:TextBox>
            </td>
            <td>Report Type
                <asp:DropDownList ID="ddlReportType" AutoPostBack ="true"  runat ="server" >
                    <asp:ListItem >--</asp:ListItem>
                    <asp:ListItem >Late Arrival</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Width="30%" />
                <%--<asp:Button ID="btnLateSearch" runat ="server" Text ="Late Arrival Report" OnClick ="btnLateSearch_Click" />--%>
                <asp:Button ID="btnExprot" runat="server" Text="Export to Excel" OnClick="btnExprot_Click" BackColor="#9966FF" EnableTheming="True" ForeColor="White"/>
            </td>
        </tr>
       
        <tr>
            <td colspan="5">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" Visible="true" OnRowDataBound="GridView1_RowDataBound" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="ASSIGNED_EMPID" HeaderText="Employee Code" SortExpression="ASSIGNED_EMPID" />

                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" SortExpression="Emp_Name" />
                     
                        <asp:BoundField DataField="Att_Date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date of Attendance" HtmlEncode="False" SortExpression="Att_Date" />
                        <asp:BoundField DataField="Intime" HeaderText="In Time" SortExpression="Intime" />
                        <asp:BoundField DataField ="InRemark" HeaderText="IN Remark" SortExpression="InRemark" />
                        <asp:BoundField DataField="Outtime" HeaderText="Out Time" SortExpression="Outtime" />
                        <asp:BoundField  DataField ="OutRemark" HeaderText="Out Remark" SortExpression="OutRemark" />
                        <asp:BoundField DataField="Totaltime" HeaderText="Total Time" SortExpression="Totaltime" />
                     
                    </Columns>
                </asp:GridView>

                <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT top 60  YANTRA_EMPLOYEE_MAST.EMP_FIRST_NAME, Emp_Attendance.Department, Emp_Attendance.Att_Date, Emp_Attendance.Intime, Emp_Attendance.Outtime, Emp_Attendance.Totaltime, Emp_Attendance.OutRemark, Emp_Attendance.InRemark, YANTRA_COMP_PROFILE.CP_FULL_NAME FROM EMP_BIO_MAP INNER JOIN Emp_Attendance ON EMP_BIO_MAP.emp_code = Emp_Attendance.Emp_Code INNER JOIN YANTRA_EMPLOYEE_MAST ON EMP_BIO_MAP.app_emp_id = YANTRA_EMPLOYEE_MAST.EMP_ID INNER JOIN YANTRA_COMP_PROFILE ON YANTRA_EMPLOYEE_MAST.CP_ID = YANTRA_COMP_PROFILE.CP_ID"></asp:SqlDataSource>--%>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
     
    <table id="tblDetailsAtt" runat="server" visible ="false" >
         <tr>
            <td colspan="3" class="profilehead">Attendence Details of Lastest Date </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;
            </td>
        </tr>
        <%--         <tr>
            <td style="text-align: right">Company Name : </td>
            <td colspan="3" style="text-align: left">
                  <asp:DropDownList ID="ddlCompanyName" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" Width="270px">
                                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                                
            </td>
        </tr>--%>
        <tr>
            <td style="text-align: right">
                Location :
            </td>
            <td colspan="2" style="text-align: left">
                <asp:DropDownList ID="ddlLoc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="2">Bangalore</asp:ListItem>
                    <asp:ListItem Value="1">Hyderabad</asp:ListItem>
                    <asp:ListItem Value="4">Mumbai</asp:ListItem>
                    <asp:ListItem Value="3">Vijayawada</asp:ListItem>
                    <asp:ListItem Value="5">Vizag</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right">Total Time : 
            </td>
            <td colspan="2" style="text-align: left">
                <asp:TextBox ID="txtTotalTime" Width="50px" runat="server"></asp:TextBox>
                <asp:Button ID="btnTotalGo" runat="server" Width="30px" Text="GO" OnClick="btnTotalGo_Click" />
            </td>
        </tr>
        <tr>
            <td style="text-align: right">In Time : 
            </td>
            <td colspan="2" style="text-align: left">
                <asp:TextBox ID="txtInTime" Width="50px" runat="server"></asp:TextBox>
                <asp:Button ID="btnInGo" runat="server" Width="30px" Text="GO" OnClick="btnInGo_Click" />

            </td>
        </tr>
        <tr>
            <td style="text-align: right">Out Time : 
            </td>
            <td colspan="2" style="text-align: left">
                <asp:TextBox ID="txtOutTime" Width="50px" runat="server"></asp:TextBox>
                <asp:Button ID="btnOutGo" runat="server" Width="30px" Text="GO" OnClick="btnOutGo_Click" />

            </td>
        </tr>
        
    </table>
    
</asp:Content>

