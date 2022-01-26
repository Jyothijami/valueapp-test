<%@ Page Language="C#" MasterPageFile="~/MPs/HRMP1.master" AutoEventWireup="true"
    CodeFile="EmployeeMaster.aspx.cs" Inherits="Modules_HRManagement_EmployeeMaster" Title="|| Value App : HR Management : Employee Master ||" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
          <%--  <script>
                $(function () {
                    $("[name$='txtDateOfAppointment'],[name$='txtDateOfTermination']").datepicker();
                });
            </script>--%>
            <table class="pagehead" style="width: 100%">
                <tr>
                    <td colspan="3" style="text-align: left">Employee Master</td>
                   <%-- <td style="text-align: right">
                        <asp:Button ID="btnLeaveMaster" runat="server" Style="background-color: #c4ccad; color: #000000" Text="Leave Master" OnClick="btnLeaveMaster_Click" />
                    </td>--%>
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
            </table>
            <br />
            <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="searchhead" colspan="4" style="text-align: left; width: 100%;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: left" colspan="2">
                                    Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                    <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                </td>
                                
                                <td style="text-align: right">
                                    <table border="0" cellpadding="0" cellspacing="0" align="right">
                                        <tr align="right">
                                            <td>
                                                <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                    Text="Search By " meta:resourcekey="Label14Resource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" meta:resourcekey="ddlSearchByResource1">
                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="--"></asp:ListItem>
                                                    <asp:ListItem Value="EMP_ID">Emp Id</asp:ListItem>
                                                    <asp:ListItem Value="EMP_FIRST_NAME" meta:resourcekey="ListItemResource2" Text="First Name"></asp:ListItem>
                                                    <asp:ListItem Value="EMP_LAST_NAME" meta:resourcekey="ListItemResource3" Text="Last Name"></asp:ListItem>
                                                    <asp:ListItem Value="EMP_PHONE" meta:resourcekey="ListItemResource4" Text="Phone No."></asp:ListItem>
                                                    <asp:ListItem Value="EMP_MOBILE" meta:resourcekey="ListItemResource5" Text="Mobile No."></asp:ListItem>
                                                    <asp:ListItem Value="EMP_EMAIL" meta:resourcekey="ListItemResource6" Enabled="False" Text="E-Mail"></asp:ListItem>
                                                    <asp:ListItem Value="CP_FULL_NAME" meta:resourcekey="ListItemResource7" Text="Company"></asp:ListItem>
                                                    <asp:ListItem Value="STATUS" meta:resourcekey="ListItemResource15" Text="Status(1 or 0)"></asp:ListItem>
                                                    <asp:ListItem Value="DEPT_NAME" meta:resourcekey="ListItemResource7" Text="Department"></asp:ListItem>
                                               
                                                     </asp:DropDownList></td>
                                            <td>
                                                <asp:DropDownList ID="ddlCurrentTasksSymbols" runat="server" AutoPostBack="True"
                                                    CssClass="textbox" Visible="False" Width="50px" meta:resourcekey="ddlCurrentTasksSymbolsResource1">
                                                    <asp:ListItem Selected="True" meta:resourcekey="ListItemResource8">=</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource9">&lt;</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource10">&gt;</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource11">&lt;=</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource12">&gt;=</asp:ListItem>
                                                    <asp:ListItem meta:resourcekey="ListItemResource13">R</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Label ID="lblCurrentFromDate" runat="server" CssClass="label" Font-Bold="True"
                                                    ForeColor="White" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCurrentDayTasksFromDate" runat="server" CssClass="textbox" Visible="False" meta:resourcekey="txtCurrentDayTasksFromDateResource1"></asp:TextBox>
                                                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Calendar.png"
                                                    Visible="False" meta:resourcekey="Image9Resource1"></asp:Image>

                                            </td>
                                            <td>
                                                <asp:Label ID="lblCurrentToDate" runat="server" CssClass="label" Font-Bold="True"
                                                    ForeColor="White" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCurrentDayTaskSearchText" runat="server" CssClass="textbox" meta:resourcekey="txtCurrentDayTaskSearchTextResource1"></asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                    Visible="False" meta:resourcekey="imgCurrentDayTasksToDateResource1"></asp:Image></td>
                                            <td>
                                                <asp:Button ID="btnCurrentDayTasksGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnCurrentDayTasksGo_Click" meta:resourcekey="btnCurrentDayTasksGoResource1" /></td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                        Visible="False"></asp:Label><asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                            Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label><asp:Label
                                                ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; width: 978px;">
                        <asp:GridView ID="gvEmployeeMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="true"
                            DataKeyNames="EMP_ID" OnRowDataBound="gvEmployeeMaster_RowDataBound" meta:resourcekey="gvEmployeeMasterResource1" OnDataBound="gvEmployeeMaster_DataBound" Width="100%" OnPageIndexChanging="gvEmployeeMaster_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderText="Sl.No" SortExpression="Sl.No" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
                                <asp:BoundField DataField="EMP_ID" SortExpression="" HeaderText="Emp Id">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField ReadOnly="True" DataField="EMP_ID" SortExpression="EMP_ID" HeaderText="EMPIDHidden" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
                                <asp:TemplateField SortExpression="EMP_FIRST_NAME" HeaderText="First Name" meta:resourceKey="TemplateFieldResource1">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# Bind("EMP_FIRST_NAME") %>' ID="TextBox1" meta:resourcekey="TextBox1Resource1" ForeColor="Blue"></asp:TextBox>


                                    </EditItemTemplate>

                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEmpFirstName" OnClick="lbtnEmpFirstName_Click" runat="server" Text='<%# Bind("EMP_FIRST_NAME") %>' CausesValidation="False" ForeColor="Blue" meta:resourcekey="lbtnEmpFirstNameResource1" __designer:wfdid="w6"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EMP_MIDDLE_NAME" Visible="False" SortExpression="EMP_MIDDLE_NAME" HeaderText="Middle Name" meta:resourceKey="BoundFieldResource3">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMP_LAST_NAME" SortExpression="EMP_LAST_NAME" HeaderText="Last Name" meta:resourceKey="BoundFieldResource4">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMP_GENDER" SortExpression="EMP_GENDER" HeaderText="Gender" meta:resourceKey="BoundFieldResource5">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMP_MOBILE" SortExpression="EMP_MOBILE" HeaderText="Emp Phone No" meta:resourceKey="BoundFieldResource6"></asp:BoundField>
                                <asp:BoundField DataField="ASSIGNED_MOBILENO" SortExpression="ASSIGNED_MOBILENO" HeaderText="A.Mobile No" meta:resourceKey="BoundFieldResource7"></asp:BoundField>
                                <asp:BoundField DataField="CP_SHORT_NAME" SortExpression="CP_SHORT_NAME" HeaderText="Company" meta:resourceKey="BoundFieldResource9">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ASSIGNED_EMPID" SortExpression="ASSIGNED_EMPID" HeaderText="A.Emp  Id"></asp:BoundField>
                                <asp:BoundField HtmlEncode="False" SortExpression="EMP_DET_DOJ" DataFormatString="{0:dd/MM/yyyy}" DataField="EMP_DET_DOJ" HeaderText="DOJ"></asp:BoundField>
                                <asp:BoundField DataField="ReporingName" SortExpression="ReporingName" HeaderText="Reporting To"></asp:BoundField>
                                <asp:BoundField DataField ="DEPT_NAME" HeaderText ="Dept Name" />
                                <asp:BoundField DataField ="DESG_Name" HeaderText ="Desg Name" />
                                <asp:BoundField DataField ="Status" HeaderText ="Status" />
                            </Columns>
                            <SelectedRowStyle BackColor="LightSteelBlue" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="sdsEmployeeMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="SP_HR_EMPLOYEE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; width: 978px;"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; width: 978px;">
                        <table id="Table1" align="center">
                            <tr align="center">
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" /></td>
                                <td style="width: 58px">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False" OnClick="btnDelete_Click" meta:resourcekey="btnDeleteResource1" /></td>
                                
                            </tr>
                            <tr>
                                <td style="width: 58px">
                                    <asp:Button ID="btnImage" runat="server" OnClientClick="window.open('../Masters/EmpImage.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"
                                        Text="Add Image" CausesValidation="False"  />

                                </td>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server"
                                Text="Print" CausesValidation="False" OnClick="btnPrint_Click" />
                            </td>
                            <td>
                                    <%--<asp:Button ID="Button1" runat="server" OnClick="Button1_Click"  Text="EmpData" />--%>
                                <asp:Button ID="Button1" runat="server" OnClientClick="window.open('../HR/EmpData.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"
                                        Text="Emp Data" CausesValidation="False"  />
                            </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; width: 978px;"></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; width: 978px;">
                        <table id="tblEmployeeDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                            visible="false" width="100%">
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Professional Details :</td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right;"></td>
                                <td style="height: 21px; text-align: left;"></td>
                                <td style="height: 21px; text-align: right;"></td>
                                <td style="height: 21px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <%--<asp:Label ID="Label2" runat="server" Text="Department : " meta:resourcekey="Label2Resource1"></asp:Label>--%>
                            Department : 
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="154px" meta:resourcekey="ddlDepartmentResource1">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label31" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label31Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                                        ErrorMessage="Please Select Department " InitialValue="0" meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right;">
                                    <%--<asp:Label ID="Label3" runat="server" Text="Date Of Appointment :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label>--%>
                            Date Of Appointment : 
                                </td>
                                <td style="text-align: left;" align="left">
                                    <asp:TextBox ID="txtDateOfAppointment" runat="server" type="datepic" meta:resourcekey="txtDateOfAppointmentResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDateOfAppointment"
                                        ErrorMessage="Enter Appointment" ValidationGroup="a">*</asp:RequiredFieldValidator>

                                    <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfAppointment" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                    </cc1:MaskedEditExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblDesignation" runat="server" Text="Designation :" Width="80px" meta:resourcekey="lblDesignationResource1"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlDesignation" runat="server" Width="154px" meta:resourcekey="ddlDesignationResource1">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDesignation"
                                        ErrorMessage="Please Select Designation" InitialValue="0" meta:resourcekey="RequiredFieldValidator2Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right; width: 138px;">
                                    <asp:Label ID="Label4" runat="server" Text="App Validity Date: " Width="138px" meta:resourcekey="Label4Resource1"></asp:Label>
                                    <%--Date Of Termination :--%> 
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDateOfTermination" runat="server" type="datepic" meta:resourcekey="txtDateOfTerminationResource1"></asp:TextBox>

                                    <%--<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfTermination" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                    </cc1:MaskedEditExtender>--%>
                                 <asp:Label ID="Label2" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label13Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDateOfTermination"
                                        ErrorMessage="Please Select Date of Termination" meta:resourcekey="RequiredFieldValidator3Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label5" runat="server" Text="Employee Type :" Width="107px" meta:resourcekey="Label5Resource1"></asp:Label></td>
                                <td style="text-align: left; height: 22px;">
                                    <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="154px" meta:resourcekey="ddlEmployeeTypeResource1">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label13Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEmployeeType"
                                        ErrorMessage="Please Select Employee Type" InitialValue="0" meta:resourcekey="RequiredFieldValidator3Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                <td   style="text-align: right;">
                            Date of Termination :
                        </td>
                        <td   style="text-align: left">
                            <asp:TextBox ID="txtAsset6" runat="server" type="datepic" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label21" runat="server" Text="Service Bond :"></asp:Label>
                                    <%--Service Bond :--%> 
                                </td>
                                <td style="text-align: left; height: 22px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdbYes" runat="server" Text="Yes" AutoPostBack="True" GroupName="ab" OnCheckedChanged="RadioButton1_CheckedChanged"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbno" runat="server" Text="No" AutoPostBack="True" Checked="True" GroupName="ab" OnCheckedChanged="rdbno_CheckedChanged"></asp:RadioButton></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" style="text-align: right; height: 22px;">
                                    <%--<asp:Label ID="Label26" runat="server" Text="Company : " meta:resourcekey="Label26Resource1"></asp:Label>--%>
                            Company : 
                                </td>
                                <td style="text-align: left; height: 22px;">
                                    <%--<asp:DropDownList ID="ddlCompany" runat="server" DataSourceID="compsds1" meta:resourcekey="ddlCompanyResource1">
                            </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="compsds1" DataTextField="COMP_NAME" DataValueField="CP_ID">
                                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCompany"
                                        ErrorMessage="Please select Company" InitialValue="0" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblYears" runat="server" Text="Years :" Visible="False"></asp:Label>
                                    <asp:Label ID="lblReportId" runat ="server" Text ="Reporting To :"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtYears" runat="server" Visible="False"></asp:TextBox>
                                    <asp:DropDownList ID="ddlReportingTo" runat ="server" ></asp:DropDownList>
                                </td>
                                <td align="right">
                                    <%--<asp:Label ID="Label23" runat="server" Text="Assigned Email Id : "></asp:Label>--%>
                            Assigned Email Id : 
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAssignedEmail" runat="server" meta:resourcekey="txtEmailResource1">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label35" runat="server" Text="Assigned Employee Id :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAssignedEmpId" runat="server" meta:resourcekey="txtEmailResource1">
                                    </asp:TextBox>
                                    <asp:Label ID="Label38" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label1Resource1">
                                    </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAssignedEmpId"
                                        ErrorMessage="Please Enter Assigned Employee Id" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtAssignedEmpId" Enabled="True">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td align="right" style="text-align: right; height: 22px;">
                                    <asp:Label ID="Label20" runat="server" Text="Assigned Mobile No :"></asp:Label>
                                </td>
                                <td style="text-align: left; height: 22px;">
                                    <asp:TextBox ID="txtAssignedMobile" runat="server" meta:resourcekey="txtMobileNoResource1">
                                    </asp:TextBox>
                                     <cc1:FilteredTextBoxExtender ID="ftxteMobileNo2" runat="server" TargetControlID="txtAssignedMobile"
                                        ValidChars="-0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <%--<cc1:FilteredTextBoxExtender
                                        ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtAssignedMobile" Enabled="True">
                                    </cc1:FilteredTextBoxExtender>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label32" runat="server" Text="Bank Name :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBankName" runat="server" meta:resourcekey="txtEmailResource1">
                                    </asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label46" runat="server" Text="Insurance Company Name :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtInsuranceCompany" runat="server" TextMode="SingleLine" meta:resourcekey="txtEmailResource1">
                                    </asp:TextBox></td>
                            </tr>

                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label36" runat="server" Text="Assigned Acc No :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAssignedAccNo" runat="server" meta:resourcekey="txtEmailResource1">
                                    </asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label37" runat="server" Text="Insurance No :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtInsurance" runat="server" TextMode="SingleLine" meta:resourcekey="txtEmailResource1">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label41" runat="server" Text="CTC P.A :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtGrosssalary" runat="server">0</asp:TextBox>

                                    <asp:Label ID="Label3" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label1Resource1">
                                    </asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtGrosssalary"
                                        ErrorMessage="Please Enter Employee CTC(P.A)" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label42" runat="server" Text="PF.No:" Width="72px" meta:resourcekey="lblpnoResource1">
                                    </asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPerPhoneNo" runat="server" meta:resourcekey="txtPhoneNoResource1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="Label39" runat="server" Text="Status :"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rdbActive" runat="server" Text="Active" GroupName="abc" OnCheckedChanged="RadioButton1_CheckedChanged" Checked="True"></asp:RadioButton>
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="rdbInactive" runat="server" Text="Inactive" GroupName="abc" OnCheckedChanged="rdbno_CheckedChanged"></asp:RadioButton></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">
                                    <asp:Label ID="Label33" runat="server" Text="Assigned Branch :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlBranch" runat="server" Width="154px" meta:resourcekey="ddlBranchResource1">
                                    </asp:DropDownList><asp:Label ID="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False" meta:resourcekey="Label16Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="ddlBranch" ErrorMessage="Please Select Branch" InitialValue="0" Enabled="False" meta:resourcekey="RequiredFieldValidator5Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator>

                                </td>
                            </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead">Employee Assets :</td>
                    </tr>
                    <tr>
            <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                           Basic (35/45)%  :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtAsset1" runat="server"></asp:TextBox>
                        </td>
                        <td   style="text-align: right;">
                           Asset 3 :
                        </td>
                        <td   style="text-align: left">
                            <%--<asp:TextBox ID="txtAsset2" runat="server" ></asp:TextBox>--%>
                             <asp:TextBox ID="txtAsset3" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                             select here if there is no PF:
                           
                        </td>
                        <td style="text-align: left;">
                           <asp:RadioButton ID="txtAsset2" runat="server" Text ="No PF"  />
                           
                        </td>
                        <td   style="text-align: right;">
                            Exclude Paysheet :
                        </td>
                        <td   style="text-align: left">
                            <%--<asp:TextBox ID="txtAsset4" runat="server" ></asp:TextBox>--%>
                           <asp:RadioButton ID="txtAsset4" runat="server" Text ="Exclude Paysheet"  />

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            Bonus :
                        </td>
                        <td style="text-align: left;">
                            <%--<asp:TextBox ID="txtAsset5" runat="server"></asp:TextBox>--%>
                           <asp:RadioButton ID="txtAsset5" runat="server" Text ="No Bonus"  />

                            </td>
                        
                    </tr>

                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">Employee Details :</td>
                            </tr>
                            <tr>
                                <td style="text-align: right;"></td>
                                <td style="text-align: left;"></td>
                        <td   style="text-align: right;"></td>
                        <td   style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label7" runat="server" Text="First Name :" Width="81px" meta:resourcekey="Label7Resource1"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtFirstName" runat="server" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>
                                    <asp:Label ID="Label17" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label17Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtFirstName"
                                        ErrorMessage="Please Enter First Name" meta:resourcekey="RequiredFieldValidator6Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td   style="text-align: right;">
                                    <asp:Label ID="lblEmployeeName" runat="server" Text="Middle Name :" Width="96px" meta:resourcekey="lblEmployeeNameResource1"></asp:Label></td>
                        <td   style="text-align: left">
                                    <asp:TextBox ID="txtMiddleName" runat="server" meta:resourcekey="txtMiddleNameResource1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label6" runat="server" Text="Last Name :" Width="82px" meta:resourcekey="Label6Resource1"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtLastName" runat="server" meta:resourcekey="txtLastNameResource1"></asp:TextBox>
                                    <asp:Label ID="Label18" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label18Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLastName"
                                        ErrorMessage="Please Enter Last Name" meta:resourcekey="RequiredFieldValidator7Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                        <td   style="text-align: right;">
                                    <asp:Label ID="Label8" runat="server" Text="Gender :" Width="59px" meta:resourcekey="Label8Resource1"></asp:Label></td>
                        <td   style="text-align: left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="rbtMale" runat="server" GroupName="gender" Text="Male" Checked="True" meta:resourcekey="rbtMaleResource1" /></td>
                                            <td>
                                                <asp:RadioButton ID="rbtFemale" runat="server" GroupName="gender" Text="Female" meta:resourcekey="rbtFemaleResource1" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label10" runat="server" Text="Date Of Birth :" Width="90px" meta:resourcekey="Label10Resource1"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDateOfBirth" runat="server" type="datepic" meta:resourcekey="txtDateOfBirthResource1"></asp:TextBox>
                                     <asp:Label ID="Label23" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label18Resource1"></asp:Label>
                                   
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDateOfBirth"
                                        ErrorMessage="Please Enter DOB" meta:resourcekey="RequiredFieldValidator7Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                   --%><%-- <asp:Image ID="imgDob" runat="server" ImageUrl="~/Images/Calendar.png" meta:resourcekey="imgDobResource1" />
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="CalendarExtender2" runat="server" Enabled="True"
                                        PopupButtonID="imgDob" TargetControlID="txtDateOfBirth">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" DisplayMoney="Left"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfBirth" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                    </cc1:MaskedEditExtender>--%>
                                </td>
                                <td rowspan="3" style="text-align: right;">
                                    <asp:Label ID="Label40" runat="server" Text="Image :"></asp:Label></td>
                                <td rowspan="3" style="text-align: left">
                                    <asp:Image ID="Image1" runat="server" Height="120px" ImageUrl="~/Images/noimage400x300.gif"
                                        Width="147px"></asp:Image></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label15" runat="server" Text="Father/Husband Name :" Width="143px"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtfathername" runat="server">
                                    </asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label11" runat="server" Text="Email :" Width="41px" meta:resourcekey="Label11Resource1"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtEmail" runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox><asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                        ErrorMessage="Please Enter E-Mail In Correct Format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator1Resource1" ValidationGroup="a">*</asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="2" style="height: 19px; text-align: left">Address for Correspondence</td>
                                <td class="profilehead" colspan="2" style="height: 19px; text-align: left">Permanent Address</td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblLocalAddress" runat="server" Text="Address :" Width="81px" meta:resourcekey="lblLocalAddressResource1"></asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" meta:resourcekey="txtAddressResource1" Height="47px"></asp:TextBox><asp:Label ID="Label19" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label19Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAddress"
                                        ErrorMessage="Please Enter Address" meta:resourcekey="RequiredFieldValidator8Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator></td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label12" runat="server" Text="Address :"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtCity" runat="server" meta:resourcekey="txtCityResource1" Height="54px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblpno" runat="server" Text="Phone No :" Width="72px" meta:resourcekey="lblpnoResource1">
                                    </asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtPhoneNo" runat="server" meta:resourcekey="txtPhoneNoResource1"></asp:TextBox><cc1:FilteredTextBoxExtender
                                        ID="ftxtQuantity" runat="server" FilterType="Numbers" TargetControlID="txtPhoneNo" Enabled="True">
                                    </cc1:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label34" runat="server" Text="Mobile No :" Width="81px" meta:resourcekey="Label9Resource1">
                                    </asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtPerMobileNo" runat="server" meta:resourcekey="txtMobileNoResource1"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label9" runat="server" Text="Mobile No :" Width="81px" meta:resourcekey="Label9Resource1">
                                    </asp:Label></td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtMobileNo" runat="server" ></asp:TextBox><asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label22Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtMobileNo"
                                            ErrorMessage="Please Enter Mobile No." meta:resourcekey="RequiredFieldValidator13Resource1" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                     <cc1:FilteredTextBoxExtender ID="ftxteMobileNo" runat="server" TargetControlID="txtMobileNo" ValidChars="-0123456789">
                                    </cc1:FilteredTextBoxExtender>
                      
                                </td>
                                <td style="text-align: right;">&nbsp;</td>
                                <td style="text-align: left">&nbsp;</td>
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
                                <td style="text-align: right;"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="height: 19px; text-align: left">Emergency Contact :</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label27" runat="server" Text="Name"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtemrName" runat="server"></asp:TextBox></td>
                                            <td style="text-align: right" rowspan="3">
                                                <asp:Label ID="Label30" runat="server" Text="Address"></asp:Label></td>
                                            <td rowspan="3" style="width: 268px">
                                                <asp:TextBox ID="txtemrAddress" runat="server" TextMode="MultiLine" Height="69px" EnableTheming="True" Width="248px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label28" runat="server" Text="Relationship"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtemrRelationship" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label29" runat="server" Text="Phone"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtemrPhone" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="profilehead" colspan="4" style="text-align: left">Documents Submitted</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="4" style="text-align: center; " class="auto-style1">
                                                <asp:CheckBoxList ID="chklDocumentsSubmitted" runat="server" AutoPostBack ="true"  RepeatColumns="4" RepeatDirection="Horizontal"
                                                    Width="100%" OnSelectedIndexChanged="chklDocumentsSubmitted_SelectedIndexChanged">
                                                    <asp:ListItem >VoterId Card</asp:ListItem>
                                                    <asp:ListItem>Driving License</asp:ListItem>
                                                    <asp:ListItem>Ration Card</asp:ListItem>
                                                    <asp:ListItem>PassPort</asp:ListItem>
                                                    <asp:ListItem>Pan</asp:ListItem>
                                                    <asp:ListItem>Adhar Card</asp:ListItem>
                                                    <asp:ListItem>Telephone Bill</asp:ListItem>
                                                    <asp:ListItem>Bank Pass Book</asp:ListItem>
                                                    <asp:ListItem>Roles & Responsibilities</asp:ListItem>

                                                </asp:CheckBoxList>
                                                <asp:TextBox ID="txtVoterId" runat ="server" PlaceHolder="VoterId No" Visible ="false" ></asp:TextBox>
                                                <asp:TextBox ID="txtDL" runat ="server" PlaceHolder="Driving License No" Visible ="false" ></asp:TextBox>
                                                <asp:TextBox ID="txtRation" runat ="server" PlaceHolder="Ration Card No" Visible ="false" ></asp:TextBox>
                                                <asp:TextBox ID="txtPassBook" runat ="server" PlaceHolder="PassBook No" Visible ="false" ></asp:TextBox>

                                                <asp:TextBox ID="txtPan" runat ="server" PlaceHolder="Pan Card No" Visible ="false" ></asp:TextBox>
                                                <asp:TextBox ID="txtAdhar" runat ="server" PlaceHolder="Adhar Card No" Visible ="false" ></asp:TextBox>
                                                <asp:TextBox ID="txtTelBill" runat ="server" PlaceHolder="Telephone Bill No" Visible ="false" ></asp:TextBox>
                                                <asp:TextBox ID="txtBPB" runat ="server" PlaceHolder="Bank Pass Book" Visible ="false" ></asp:TextBox>

                                                <asp:Label ID="lblchkllist" runat="server" Text="Please Check atleast 3 documents"
                                                    Visible="False" Font-Bold="True" ForeColor="Red" EnableTheming="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="width: 852px; text-align: center">
                                                <asp:CheckBox ID="chkothers" runat="server" AutoPostBack="True" OnCheckedChanged="chkothers_CheckedChanged"
                                                    Text="Others"></asp:CheckBox>
                                                <asp:TextBox ID="txtOthers" runat="server" Visible="False">
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                    <td style="text-align: center">Upload Documents :
                                                <asp:FileUpload ID="docSubmitted" runat="server" AllowMultiple="true" OnDataBinding="Upload_DataBinding" />
                                            </td>
                                        </tr>
                                         <tr>
                                    <td style="text-align: center">Employee Image :&nbsp;&nbsp;&nbsp;
                                        <asp:FileUpload ID="EmpImage" runat="server" AllowMultiple="false" />
                                            </td>
                                        </tr>
                                        <tr>
                        
                        <td style="text-align: center"><asp:Label ID="Label56" runat="server" Visible="false" Text="Attached File"></asp:Label>&nbsp;
                            <asp:LinkButton ID="lbtnAttachedFiles" runat="server" Visible="False"
                                OnClick="lbtnAttachedFiles_Click"></asp:LinkButton>
                            <asp:Repeater ID="UploadsRepeater" Visible="false" runat="server" DataSourceID="sdsUploads">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnFileOpener" CausesValidation="False" runat="server"
                                        OnClick="lbtnFileOpener_Click" Text='<%# Bind("Document_Submitted") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:SqlDataSource ID="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                SelectCommand="SELECT * FROM [Emp_Documents_Submitted] WHERE (EMP_ID=@SO_IDpara) Order by Doc_Id Desc">
                                <SelectParameters>
                                    <asp:ControlParameter PropertyName="Text" DefaultValue="0" Name="SO_IDpara"
                                        ControlID="lblSOIdHidden"></asp:ControlParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:Label ID="lblSOIdHidden" runat="server" Visible="False"></asp:Label></td>

                    </tr>
                                        <tr>
                        
                        <td style="text-align: center">Attachments :
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="Repeater1" DataSourceID="sdsUploads" runat="server">
                                        <HeaderTemplate>
                                            <table cellspacing="0" rules="all" border="1">
                                                <tr>
                                                    <th scope="col" style="width: 120px">File Id
                                                    </th>
                                                    <th scope="col" style="width: 100px">File Name
                                                    </th>
                                                    <th scope="col" style="width: 100px">Doc Type
                                                    </th>
                                                    <th scope="col" style="width: 80px"></th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_So_Upload_Id" runat="server" Text='<%# Eval("DOC_ID") %>' Visible="true" />

                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnFileOpener2" CausesValidation="False" runat="server"
                                                        OnClick="lbtnFileOpener2_Click" Text='<%# Bind("Document_Submitted") %>'></asp:LinkButton>

                                                    <%--<asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>' />--%>
                                            
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label26" runat="server" Text='<%# Eval("Emp_Doc_ID") %>' Visible="true" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDelete" OnClientClick='javascript:return confirm("Are you sure you want to delete?")'  Text="Delete" runat="server" OnClick="lnkDelete_Click" CausesValidation="false" />


                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: left" class="profilehead">User Name :</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right;"></td>
                                <td style="height: 19px; text-align: left;"></td>
                                <td style="height: 19px; text-align: right;"></td>
                                <td style="height: 19px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="Label25" runat="server" Text="User Name :" Width="81px" meta:resourcekey="Label25Resource1"></asp:Label></td>
                                <td style="text-align: left;" colspan="3">
                                    <asp:TextBox ID="txtUserName" runat="server" meta:resourcekey="txtUserNameResource1"></asp:TextBox><asp:Label ID="Label24" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label24Resource1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtUserName"
                                        ErrorMessage="Please Enter User Name" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="User Name Should Not be With Space & Please Do not Update User name after creation of User id in 'Add user' Page" Visible="true">

                                    </asp:Label>
                                    <asp:Label ID="lblUserName" runat="server" Visible="false"></asp:Label></td>
                                
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right;"></td>
                                <td style="height: 19px; text-align: left;"></td>
                                <td style="height: 19px;"></td>
                                <td style="height: 19px"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table id="Table2" style="width: 1px; height: 1px" align="center">
                                        <tr align="center">
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" ValidationGroup="a" /></td>
                                            <td>
                                                <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" Text="Refresh" OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource1" /></td>
                                            <td>
                                                <asp:Button ID="btnClose" runat="server" CausesValidation="False" Text="Close" OnClick="btnClose_Click" meta:resourcekey="btnCloseResource1" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="7"></td>
                </tr>
            </table>
            <asp:SqlDataSource ID="compsds1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT a.CP_ID, a.CP_FULL_NAME + ',' + b.locname AS COMP_NAME FROM YANTRA_COMP_PROFILE AS a INNER JOIN location_tbl AS b ON a.locid = b.locid"></asp:SqlDataSource>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                ShowMessageBox="True" ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="a" meta:resourcekey="ValidationSummary1Resource1" />
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            width: 852px;
            height: 68px;
        }
    </style>
</asp:Content>


