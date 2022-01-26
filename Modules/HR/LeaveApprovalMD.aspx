<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveApprovalMD.aspx.cs" Inherits="Modules_HR_LeaveApprovalMD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4">EMPLOYEE LEAVE APPROVAL :</td>
        </tr>
    </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">LEAVE APPROVAL</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 100%;">
                <asp:GridView ID="gvEnrollmentDtls" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%">
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
                        <asp:TemplateField HeaderText="Employee Id" >
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblId" Text='<%#Eval("Emp_Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblName" Text='<%#Eval("EMP_FIRST_NAME")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days of Leave">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDaysofLeave" Text='<%# Eval("DayofLeave") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFromDate" Text='<%#Eval("FromDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblToDate" Text='<%#Eval("ToDate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason For Leave">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblReason" Text='<%# Eval("ReasonforLeave") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Type of Leave">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLeaveType" Text='<%#Eval("LeaveType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Applied Date">
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAppliedDate" Text='<%# Eval("Leave_Date") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand=" 
  select a.Leave_Id, a.[Emp_Id],b.[EMP_FIRST_NAME],a.[DayofLeave],convert(varchar(10),a.[FromDate],103) as FromDate,convert(varchar(10),a.[ToDate],103) as ToDate,
  convert(varchar(10),a.[Leave_Date],103) as Leave_Date,a.[ReasonforLeave],a.[LeaveType] from [YANTRA_LEAVE_FORM] a 
  inner join [YANTRA_EMPLOYEE_MAST] b on a.[Emp_Id]=b.[EMP_ID] where a.Status2='Approved' and a.Status3='Pending'"></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <table id="Table1" align="center">
                    <tr align="center">
                        <td> 
                        </td>
                        <td>&nbsp;</td>
                        <td style="width: 58px">
                                                        <asp:Button ID="btnApprove" runat="server"  Text="Approve" OnClick="btnApprove_Click" /></td>
                        <td style="width: 58px">
                                                        <asp:Button ID="btnreject" runat="server"  Text="Reject" OnClick="btnreject_Click" /></td>
                            
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

