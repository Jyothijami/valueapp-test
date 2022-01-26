<%@ Page Title="||Value App : Leave Master : MD Approval ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="MDApproval.aspx.cs" Inherits="Modules_HR_MDApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">

        <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">MD Pending Leave Applications</td>
        </tr>
    </table>
    <div id="divLeaveApp">
        <table style="width: 100%">
            <tr>
                <td style="text-align:center">
                    <asp:GridView ID="gvEnrollmentDtls"  runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                             <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr"  runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged"/>
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave Id" Visible="false" >
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLeaveId" Text='<%#Eval("Leave_Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee Id" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("Emp_Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblName" Text='<%#Eval("Emp_Name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDesignation" Text='<%#Eval("Emp_Designation")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Department">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDepartment" Text='<%#Eval("Emp_Department")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Applied Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateApply" Text='<%#Eval("Applied Date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("From Date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField HeaderText="From Session" DataField="FromSession" />
                        <asp:TemplateField HeaderText="To Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("To Date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:BoundField HeaderText="To Session" DataField="ToSession" />
                          <asp:TemplateField HeaderText="Applied No Of Leaves">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNoOfDays" Text='<%#Eval("AppliedNoOfLeaves")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type Of Leave">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTypeOfLeave" Text='<%#Eval("TypeOfLeave")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblReason" Text='<%#Eval("Reason")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address In Leave Period">
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
                <td style="text-align:center">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" />
                    &nbsp;<asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" />
                    &nbsp;<asp:Button ID="btnLeaveHistory" runat="server" Text="Leave History" OnClick="btnLeaveHistory_Click"  />


                    &nbsp;<asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Obsolete Leave" />


                </td>
            </tr>
        </table>
        

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT Leave_Id, Emp_Id, 
            Emp_Name, Emp_Designation, Emp_Department, CONVERT (VARCHAR(10), DateOfApplying, 103) AS [Applied Date], CONVERT (VARCHAR(10), FromDate, 103) AS [From Date], 
            CONVERT (VARCHAR(10), ToDate, 103) AS [To Date], AppliedNoOfLeaves, TypeOfLeave, Reason, AddressInLeavePeriod,FromSession,ToSession,[EMP_Leave_tbl].Comment2 FROM EMP_Leave_tbl WHERE (Status3='Pending' and Status2 = 'Approved' and Status1='Approved')">

        </asp:SqlDataSource>

    </div>
    <br />
        <table>
        <tr>
            <td>
                Employee Name :<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox> &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <div id="divGV">
        <asp:GridView ID="gvLeaveHistory" CssClass="gvStyle" runat="server" Width="100%" AllowPaging="True" OnPageIndexChanging="gvLeaveHistory_PageIndexChanging"></asp:GridView>
    </div>
    <asp:Label ID="lblMD_Name" runat="server" Visible="false"></asp:Label>

</asp:Content>

