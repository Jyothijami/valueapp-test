<%@ Page Title="||Value App : HR : EmployeeEnroll ||" Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true" 
    CodeFile="EmployeeEnrollment.aspx.cs" Inherits="Modules_HR_EmployeeEnrollment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .auto-style1 {
            height: 52px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
        
    <table class="pagehead" style="width: 100%">
        <tr>
            <td colspan="4" style="text-align: left;">Employee Enrollment</td>
      <%--      <td>
                <asp:Button ID="btnIntSch" runat="server" Text="Employee Interview Schedule" PostBackUrl="~/Modules/HR/Emp_Interview.aspx" /></td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Employee Interview Result" PostBackUrl="~/Modules/HR/Emp_InterviewResult.aspx" /></td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Employee Offer Letter" PostBackUrl="~/Modules/HR/Emp_OfferLetter.aspx" /></td>--%>
        </tr>
    </table>


    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">Employee Enrollment</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <asp:GridView ID="gvEnrollmentDtls" AutoGenerateColumns="False" runat="server" EmptyDataText="No Records Found" DataSourceID="SqlDataSource1" Width="100%" AllowSorting="True">
                    <Columns>
                        <asp:TemplateField>
                             <HeaderTemplate>
                                <asp:CheckBox ID="chkhdr"  runat="server" AutoPostBack="True" OnCheckedChanged="chkhdr_CheckedChanged"/>
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="Chk"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
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
                        <asp:TemplateField HeaderText="Education" SortExpression="Education">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblEducation" Text='<%# Eval("Education") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

                <br />
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT [EnrollmentId], [FirstName], [LastName], [MobileNo], [EmailId], [Education] FROM [Emp_Enrollment] where Status='Pending' ORDER BY [EnrollmentId] DESC "></asp:SqlDataSource>

            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <table id="Table1" align="center">
                    <tr align="center">
                        <td>
                            <asp:Button ID="btnNew" runat="server" Text="New" CausesValidation="False" meta:resourcekey="btnDeleteResource1" OnClick="btnNew_Click" />&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="width: 58px">
                            <asp:Button ID="btnAccept" runat="server" Text="Accept" CausesValidation="False" meta:resourcekey="btnDeleteResource1" OnClick="btnAccept_Click" /></td>
                        <td style="width: 58px">
                            <asp:Button ID="btnReject" runat="server"
                                Text="Reject" CausesValidation="False" OnClick="btnReject_Click" /></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblEmployeeDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" width="100%">
        <tr>
            <td colspan="4" class="profilehead" style="text-align: left">Employee Details</td>
        </tr>

        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;"></td>
            <td style="text-align: left"></td>
        </tr>

        <tr>
            <td style="text-align: right;"></td>
            <td style="text-align: left;"></td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label5" runat="server" Text="Date Of Enrollment : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Date Of Enrollment : 
            </td>
            <td style="text-align: left;" align="left">
                <asp:TextBox ID="txtDate" runat="server" type="datepic" meta:resourcekey="txtDate"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                    ErrorMessage="Please Enter Appointment Date" ValidationGroup="ed">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label2" runat="server" Text="FirstName : " meta:resourcekey="Label2Resource1"></asp:Label>--%>
                FirstName : 
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtFname" runat="server"></asp:TextBox>
                <asp:Label ID="Label31" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFname"
                    ErrorMessage="Please Enter First Name" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="ed">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label3" runat="server" Text="Middle Name : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Middle Name : 
            </td>
            <td style="text-align: left;" align="left">
                <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;" class="auto-style1">
                <%--<asp:Label ID="Label1" runat="server" Text="Last Name :" meta:resourcekey="Label2Resource1"></asp:Label>--%>
                Last Name : 
            </td>
            <td style="text-align: left;" class="auto-style1">
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName"
                    ErrorMessage="Please Enter LastName " meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="ed">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right;" class="auto-style1">
                <%--<asp:Label ID="Label6" runat="server" Text="Mobile No :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Mobile No : 
            </td>
            <td style="text-align: left;" align="left" class="auto-style1">
                <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="RegularExpressionValidator" ValidationExpression="\(\d{3}\)-\d{6}" ControlToValidate="txtMobileNo">*</asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label7" runat="server" Text="EMail Id : " meta:resourcekey="Label2Resource1"></asp:Label>--%>
                EMail Id : 
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:Label ID="Label8" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Please Enter Email " meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="ed">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"  runat="server" ErrorMessage="Please Enter Valid Email Format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label9" runat="server" Text="Education : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Education : 
            </td>
            <td style="text-align: left;" align="left">
                <asp:TextBox ID="txtEducation" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label10" runat="server" Text="Address : " meta:resourcekey="Label2Resource1"></asp:Label>--%>
                Address : 
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:Label ID="Label11" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
            </td>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label12" runat="server" Text="Resume : " Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Resume :  
            </td>
            <td style="text-align: left;" align="left">
                <%--<asp:FileUpload id="fuResume" runat="server" />
    <asp:Button runat="server" id="btnUpload" text="Upload" onclick="btnUpload_Click" />--%>
                <%--#########--%>
                <asp:FileUpload ID="fuResume" runat="server" />
                <asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="fuResume"
                    ErrorMessage="Please Select Resume To Upload " meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="ed">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <%--<asp:Label ID="Label13" runat="server" Text="Date Of Birth : " meta:resourcekey="Label3Resource1"></asp:Label>--%>
                Date Of Birth : 
            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtDOB" runat="server" type="datepic"></asp:TextBox>
                <asp:Label ID="Label14" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
            </td>
            <%--<td style="text-align: right;">
                <asp:Label ID="Label15" runat="server" Text="Age : " meta:resourcekey="Label3Resource1" Visible="False"></asp:Label>
                Age : 
            </td>--%>
            <td style="text-align: left;">
                <asp:Label ID="lblAge" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <table id="Table2" style="width: 1px; height: 1px" align="center">
                    <tr align="center">
                        <td>    
                            <asp:Button ID="btnSave" runat="server" Text="Save" meta:resourcekey="btnSaveResource1" OnClick="btnSave_Click" Height="26px" ValidationGroup="ed" /></td>

                        <td>
                            <asp:Button ID="btnClose" runat="server" CausesValidation="False" Text="Close" meta:resourcekey="btnCloseResource1" OnClick="btnClose_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="USP_SearchEnrollStatus" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden2" DefaultValue="0" Name="SearchItemName2" PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden2" DefaultValue="0" Name="SearchValue2" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="profilehead" style="text-align: left">Employee Enrollment History :</td>
            <td style="text-align: right">
                 <asp:DropDownList ID="ddlNoOfRecords" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>                   
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:right">
                <table border="0" cellpadding="0" cellspacing="0" align="right">
                    <tr>
                        <td>
                            <asp:Label ID="Label27" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                Text="Search By"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem Value="Status">Status</asp:ListItem>
                            </asp:DropDownList></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                    </tr>
                </table>
                <asp:Label ID="lblSearchItemHidden2" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblSearchValueHidden2" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvHistory" runat="server" DataSourceID="SqlDataSource2" AutoGenerateColumns="False" EmptyDataText="No Records Found" DataKeyNames="EnrollmentId" Width="100%" AllowPaging="True" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="EnrollmentId" HeaderText="Enrollment Id" ReadOnly="True" Visible="false" SortExpression="EnrollmentId" />
                        <asp:BoundField DataField="FirstName" HeaderText="Firs tName" SortExpression="FirstName" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile Number" SortExpression="MobileNo" />
                        <asp:BoundField DataField="EmailId" HeaderText="Email Id" SortExpression="EmailId" />
                        <asp:BoundField DataField="Education" HeaderText="Education" SortExpression="Education" />
                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                        <asp:BoundField DataField="EnrollmentDate" HeaderText="Enrollment Date" SortExpression="EnrollmentDate" />
                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary runat="server" ID="vs1" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ></asp:ValidationSummary>
                <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="ed" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ></asp:ValidationSummary>

            </td>
        </tr>
    </table>
</asp:Content>

