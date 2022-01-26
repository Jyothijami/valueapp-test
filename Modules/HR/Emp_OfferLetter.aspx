<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" CodeFile="Emp_OfferLetter.aspx.cs" Inherits="Modules_HR_Emp_OfferLetter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 88px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Offer Letter </td>
            <td>
                <asp:Label ID="lblEnrollmentId" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblName" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblGrossSalary" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblEmailId" runat="server" Visible="false"></asp:Label>

                <asp:Label ID="lblAddress1" runat="server" Visible="False"></asp:Label>

            </td>
            <%--  <td>
                <asp:Button ID="btnEmpEnrollment" runat="server" Text="Employee Enrollment " PostBackUrl="~/Modules/HR/EmployeeEnrollment.aspx" /></td>
            <td>
                <asp:Button ID="btnEmpInterview" runat="server" Text="Employee Interview" PostBackUrl="~/Modules/HR/Emp_Interview.aspx" /></td>
            <td>
                <asp:Button ID="btnIntSch" runat="server" Text="Employee Interview Result" PostBackUrl="~/Modules/HR/Emp_InterviewResult.aspx" /></td>--%>
        </tr>
    </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Offer Details</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <asp:GridView ID="gvEnrollmentDtls" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%" OnRowCommand="gvEnrollmentDtls_RowCommand" AllowSorting="True">
                    <Columns>
                        <%--<asp:TemplateField>
                             <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr"  runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged"/>
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Enrollment Id" SortExpression="EnrollmentId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblNo" Text='<%#Eval("EnrollmentId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile Number" SortExpression="MobileNo">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMobileNo" Text='<%#Eval("MobileNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email Id" SortExpression="EmailId">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("EmailId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Education" SortExpression="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interviewer Name" SortExpression="InterviewerName">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblInterviewerName" Text='<%#Eval("InterviewerName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Of Interview" SortExpression="DateOfInterview">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDateOfInterview" Text='<%#Eval("DateOfInterview")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location" SortExpression="Location">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("Location") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gross Salary" SortExpression="GrossSalary">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGrossSalary" Text='<%#Eval("GrossSalary")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address" SortExpression="Address">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAddress" Text='<%#Eval("Address")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Approve" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Reject" Width="80px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="width: 58px; text-align: right;">
                <asp:Button ID="btnAccept" runat="server" Text="Accept" CausesValidation="False" OnClick="btnAccept_Click" Visible="False" Width="80px" /></td>
            <td style="width: 58px">
                <asp:Button ID="btnReject" runat="server"
                    Text="Reject" CausesValidation="False" OnClick="btnReject_Click" Visible="False" Width="80px" /></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.[EnrollmentId], a.[FirstName], a.[LastName], a.[MobileNo],
                     a.[EmailId], a.[Education],  a.Address,b.[InterviewerName], convert(varchar(12), b.[DateOfInterview], 103) as DateOfInterview,b.Location,
                    b.GrossSalary  FROM [Emp_Enrollment] a inner join [Emp_InterviewDetails] b 
    on a.[EnrollmentId]=b.[EnrollmentId] where b.InterviewStatus='Accepted'  order by b.[EnrollmentId]"></asp:SqlDataSource>
    <table id="tblEmployeeDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" width="100%">
        <tr>
            <td colspan="4" class="profilehead" style="text-align: left">Offer  Details</td>
        </tr>

        <tr>
            <td style="text-align: right;">&nbsp;</td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right;" class="auto-style1">
                <%--<asp:Label ID="Label5" runat="server" Text="Company Name :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Company Name : 
            </td>
            <td style="text-align: left;" class="auto-style1">
                <%--<asp:DropDownList ID="ddlCompanyName" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1">
                     <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>--%>
                <asp:DropDownList ID="ddlCompanyName" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource2" DataTextField="COMP_NAME" DataValueField="CP_ID">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>
                <%--<asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>--%>
                <asp:Label ID="Label9" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Please Select the Company Name" ValidationGroup="od" ControlToValidate="ddlCompanyName" InitialValue="0">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right;" class="auto-style1">
                <%--<asp:Label ID="Label10" runat="server" Text="Department : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Department : 
            </td>

            <td style="text-align: left; text-align: left;" class="auto-style1">
                <asp:DropDownList ID="ddlDepartment" runat="server"></asp:DropDownList>

                <asp:Label ID="Label10" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepartment" ErrorMessage="Please Select the Department Name" ValidationGroup="od" InitialValue="0">*</asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label7" runat="server" Text="Designation : " meta:resourcekey="Label2Resource1"></asp:Label>--%>
                Designation : 
            </td>
            <td style="text-align: left;">
                <asp:DropDownList ID="ddlDesignation" runat="server"></asp:DropDownList>
                <asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Please  Select The Designation " ControlToValidate="ddlDesignation" ValidationGroup="od" InitialValue="0">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label9" runat="server" Text="Date Of Joining : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Date Of Joining : 
            </td>
            <td style="text-align: left;" align="left">
                <asp:TextBox ID="txtDOJ" runat="server" type="date"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDOJ"
                    ErrorMessage="Please Enter Joining Date" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="od">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="od" />
                <asp:Button ID="btnOfferLetter" runat="server" Text="Send Offer Letter" OnClick="btnOfferLetter_Click" />

                <br />
                <br />
                <br />
                <cc2:Editor ID="Editor1" runat="server" Height="500px" Width="100%" />
            </td>

        </tr>

        <tr>
            <td colspan="4" style="text-align: center">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary runat="server" ID="vs1" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true"></asp:ValidationSummary>

                <asp:ValidationSummary runat="server" ID="vs2" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="od"></asp:ValidationSummary>


            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        
        <tr>
            <td colspan="4">
                <table style="width: 100%">
                    <tr class="profilehead">
            <td style="padding-top:16px;">Employee Offer Letter  Details History :
            </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td>
                                        <%--<asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label>--%>
                                        Search By &nbsp;
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="Status">OfferStatus</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                        </asp:TextBox>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                </tr>
                            </table>
                            <asp:Label ID="lblSearchItemHidden2" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblSearchValueHidden2" runat="server" Visible="False"></asp:Label>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="gvHistory" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource3" Width="100%" AutoGenerateColumns="False" DataKeyNames="EnrollmentId" AllowPaging="True">
                                <Columns>
                                    <asp:BoundField DataField="EnrollmentId" HeaderText="EnrollmentId" ReadOnly="True" SortExpression="EnrollmentId" />
                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" SortExpression="MobileNo" />
                                    <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
                                    <asp:BoundField DataField="Education" HeaderText="Education" SortExpression="Education" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                                    <asp:BoundField DataField="InterviewerName" HeaderText="InterviewerName" SortExpression="InterviewerName" />
                                    <asp:BoundField DataField="DateOfInterview" HeaderText="DateOfInterview" ReadOnly="True" SortExpression="DateOfInterview" />
                                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                                    <asp:BoundField DataField="GrossSalary" HeaderText="GrossSalary" SortExpression="GrossSalary" />
                                    <asp:BoundField DataField="OfferStatus" HeaderText="OfferStatus" SortExpression="OfferStatus" />
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="USP_OfferLetter_Status_Search" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblSearchItemHidden2" Name="SearchItemName2" PropertyName="Text" Type="String" DefaultValue="0" />
                                    <asp:ControlParameter ControlID="lblSearchValueHidden2" Name="SearchValue2" PropertyName="Text" Type="String" DefaultValue="0" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>

