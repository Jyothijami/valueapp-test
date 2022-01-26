<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" 
    CodeFile="Emp_Interview.aspx.cs" Inherits="Modules_HR_Emp_Interview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<asp:Label ID="Label10" runat="server" Text="Time :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 196px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
 <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Employee 1st- Interview Schedule</td>
            
        </tr>
    </table>

    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Enrolled Employees :</td>
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
                                <asp:CheckBox ID="chkhdr" runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" AutoPostBack="true" OnCheckedChanged="Chk_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enrollment Id">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo" Text='<%# Eval("EnrollmentId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MobileNo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo], a.[EmailId], a.[Education]
   FROM [Emp_Enrollment] a left outer join [Emp_InterviewDetails] b 
   on a.[EnrollmentId]=b.[EnrollmentId]
    where a.Status='Approved'  and b.[EnrollmentId] is null"></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <table id="Table1" align="center">
                    <tr align="center">
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="width: 58px">&nbsp;</td>
                        <td style="width: 58px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table id="tblEmployeeDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" width="100%">
        <tr>
            <td colspan="4" class="profilehead" style="text-align: left">Interview  Details</td>
        </tr>

        <tr>
            <td style="text-align: right;">&nbsp;</td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label5" runat="server" Text="Date Of Interview :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                            Date Of Interview :
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtDate" runat="server" type="datepic"></asp:TextBox>
                <asp:Label ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtDate"
                    ErrorMessage="Please Enter Interview Date" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label10" runat="server" Text="Time :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                            Time :
            </td>

            <td style="text-align: left; text-align: left;">
                <asp:TextBox ID="txtTime" runat="server" ValidationGroup="id"></asp:TextBox>
                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"  MaskType="Time" Mask="99:99" TargetControlID="txtTime" Filtered=":" AcceptAMPM="true" />--%>

                <%--<MKB:TimeSelector ID="TimeSelector1" runat="server" Width="180px">
                            </MKB:TimeSelector>--%>

                <track />

                <asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtTime"
                    ErrorMessage="Please Enter Interview Time" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                            Interviewer Name :
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtInterviewerName" runat="server"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInterviewerName"
                    ErrorMessage="Please Enter Interviewer Name" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id">*</asp:RequiredFieldValidator>

            </td>
            <td style="text-align: right;">
                            Location : 
            </td>
            <td style="text-align: left;" align="left">
                <asp:TextBox ID="txtLocation" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtLocation"
                    ErrorMessage="Please Enter Location" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id" ToolTip="For Company Full  Address Company select the Drop Box on top-Right Corner">*</asp:RequiredFieldValidator>
                <asp:Label ID="lblcpid" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                            Remarks :
            </td>
            <td style="text-align: left;" colspan="3">
                <asp:TextBox ID="txtRemarsks" runat="server" Width="76%"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRemarsks"
                    ErrorMessage="Please Enter Remarks" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="id" />
            </td>
        </tr>
    </table>

    <br />
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Employee 2nd- Interview Schedule</td>
            
        </tr>
    </table>

    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Enrolled Employees :</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; " class="auto-style1">
                <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource3" Width="100%">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr2" runat="server" AutoPostBack="True"  OnCheckedChanged="chkhdr2_CheckedChanged" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk2" AutoPostBack="true" OnCheckedChanged="Chk2_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enrollment Id">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo2" Text='<%# Eval("EnrollmentId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName2" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName2" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MobileNo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo2" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail2" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation2" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <br />
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo], a.[EmailId], a.[Education]
   FROM [Emp_Enrollment] a left outer join [Emp_InterviewDetails] b 
   on a.[EnrollmentId]=b.[EnrollmentId]
    where a.Status='Approved'  and b.InterviewType=1 "></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                &nbsp;
            </td>
        </tr>
    </table>

    <table id="Table2" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" width="100%">
        <tr>
            <td colspan="4" class="profilehead" style="text-align: left">Interview  Details</td>
        </tr>

        <tr>
            <td style="text-align: right;">&nbsp;</td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label5" runat="server" Text="Date Of Interview :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                            Date Of Interview :
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="TextBox1" runat="server" type="datepic"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                    ErrorMessage="Please Enter Interview Date" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id1">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label10" runat="server" Text="Time :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                            Time :
            </td>

            <td style="text-align: left; text-align: left;">
                <asp:TextBox ID="TextBox2" runat="server" ValidationGroup="id"></asp:TextBox>
                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"  MaskType="Time" Mask="99:99" TargetControlID="txtTime" Filtered=":" AcceptAMPM="true" />--%>

                <%--<MKB:TimeSelector ID="TimeSelector1" runat="server" Width="180px">
                            </MKB:TimeSelector>--%>

                <track />

                <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTime"
                    ErrorMessage="Please Enter Interview Time" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label id="Label7" runat="server" Text="Interviewer Name :" meta:resourcekey="Label2Resource1"></asp:Label>--%>
                            Interviewer Name :
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInterviewerName"
                    ErrorMessage="Please Enter Interviewer Name" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id1">*</asp:RequiredFieldValidator>

            </td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label9" runat="server" Text="Location : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                            Location : 
            </td>
            <td style="text-align: left;" align="left">
                <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" ToolTip="For Company Full  Address Company select the Drop Box on top-Right Corner"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLocation"
                    ErrorMessage="Please Enter Location" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id1">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                            Remarks :
            </td>
            <td style="text-align: left;" colspan="3">
                <asp:TextBox ID="TextBox5" runat="server" Width="76%"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox5"
                    ErrorMessage="Please Enter Remarks" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="id1">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" ValidationGroup="id1" />
            </td>
        </tr>
    </table>

    
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>

                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo], a.[EmailId],
   a.[Education],b.[InterviewerName], convert(varchar(12), b.[DateOfInterview], 103) as DateOfInterview,b.[Location]
    FROM [Emp_Enrollment] a inner join [Emp_InterviewDetails] b 
    on a.[EnrollmentId]=b.[EnrollmentId] where b.InterviewStatus='Pending' order by b.[EnrollmentId]"></asp:SqlDataSource>

            </td>
        </tr>
        <tr>

            <td colspan="2" class="profilehead">Employee Interview History :
            </td>

        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvIntRes" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource2" Width="100%">
                    <Columns>
                        <%--<asp:TemplateField>
                             <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr0"  runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged"/>
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk0" AutoPostBack="false" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Enrollment Id">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo0" Text='<%#Eval("EnrollmentId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName0" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName0" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MobileNo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo0" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="EmailId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail0" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation0" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interviewer Name">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInterviewerName" Text='<%#Eval("InterviewerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Interview">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateOfInterview" Text='<%#Eval("DateOfInterview")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("Location") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" />
                <asp:ValidationSummary ID="vs2" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="id" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="id1" />

            </td>
        </tr>
    </table>

</asp:Content>

