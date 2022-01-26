<%@ Page Title="||Value App : Leave Master : HOD Approval ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="HODApproval.aspx.cs" Inherits="Modules_HR_HODApproval" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">


    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">HOD Pending Leave Appplications</td>
            <asp:Label ID="lblDeptIds" runat ="server" Visible ="false" ></asp:Label>
        </tr>
    </table>
    <div id="divLeaveApp">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center">
                    <asp:GridView ID="gvHodPendingAppl" AutoGenerateColumns="False" runat="server" AllowSorting="true" 
                        EmptyDataText="No Records Found" Width="100%">
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

                            <asp:TemplateField HeaderText="Employee Id" SortExpression="Emp_Id" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblId" Text='<%#Eval("Emp_Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Leave Id" SortExpression="Leave_Id" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLeaveId" Text='<%#Eval("Leave_Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name" SortExpression="Emp_Name">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblName" Text='<%#Eval("Emp_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Designation" SortExpression="Emp_Designation">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDesignation" Text='<%#Eval("Emp_Designation")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Applied Date" SortExpression="Applied Date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDateApply" Text='<%#Eval("Applied Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="From Date" SortExpression="From Date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("From Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="From Session" DataField="FromSession" />
                            <asp:TemplateField HeaderText="To Date" SortExpression="To Date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("To Date")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="To Session" DataField="ToSession" />

                            <asp:TemplateField HeaderText="Applied No Of Leaves" SortExpression="AppliedNoOfLeaves">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNoOfDays" Text='<%#Eval("AppliedNoOfLeaves")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type Of Leave" SortExpression="TypeOfLeave">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblTypeOfLeave" Text='<%#Eval("TypeOfLeave")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reason" SortExpression="Reason">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblReason" Text='<%#Eval("Reason")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Address In Leave Period" SortExpression="AddressInLeavePeriod">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAddressInLeavePeriod" Text='<%#Eval("AddressInLeavePeriod")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Mobile No" SortExpression="Leave_Id" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblMobileNo" Text='<%#Eval("Comment2")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />

                    &nbsp;<asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" />
                    &nbsp;<asp:Button ID="btnLeaveHistory" runat="server" Text="Leave History" OnClick="btnLeaveHistory_Click" />
                    &nbsp;<asp:Button ID="btnDelete" runat="server" Text="Obsolete Leave" OnClick ="btnDelete_Click" />

                </td>
            </tr>
        </table>


        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT Leave_Id, Emp_Id, 
            Emp_Name, Emp_Designation, Emp_Department, CONVERT (VARCHAR(10), DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), FromDate, 103) AS [From Date], 
            CONVERT (VARCHAR(10), ToDate, 103) AS [To Date], AppliedNoOfLeaves, TypeOfLeave, Reason, AddressInLeavePeriod FROM EMP_Leave_tbl WHERE (Status1 = 'Pending')"></asp:SqlDataSource>--%>
    </div>
    <br />
    <table>
        <tr>
            <td>Employee Name :<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <div id="divGV">
        <asp:GridView ID="gvLeaveHistory" CssClass="gvStyle" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvLeaveHistory_PageIndexChanging"></asp:GridView>
    </div>
    <div>
        <asp:Label ID="lblEmpIdHidden" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblUserType" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblDeptId" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblHod_Name" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblHOD_Id" runat="server" Visible="False"></asp:Label>

    </div>
</asp:Content>

