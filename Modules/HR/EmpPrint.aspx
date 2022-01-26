<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpPrint.aspx.cs" Inherits="Modules_HR_EmpPrint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
   
      
    <table class="pagehead">
        <tr>
            <td colspan="4">
                EMPLOYEE MASTER</td>
        </tr>
    </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <br />
    
    <table style="width: 750px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="4" style="text-align: left; width: 978px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            Employee master</td>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" meta:resourcekey="Label14Resource1"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" meta:resourcekey="ddlSearchByResource1">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="--"></asp:ListItem>
                                            <asp:ListItem Value="EMP_ID">Emp Id</asp:ListItem>
                                            <asp:ListItem Value="EMP_FIRST_NAME" meta:resourcekey="ListItemResource2" Text="First Name"></asp:ListItem>
                                            <asp:ListItem Value="EMP_LAST_NAME" meta:resourcekey="ListItemResource3" Text="Last Name"></asp:ListItem>
                                            <asp:ListItem Value="EMP_PHONE" meta:resourcekey="ListItemResource4" Text="Phone No."></asp:ListItem>
                                            <asp:ListItem Value="EMP_MOBILE" meta:resourcekey="ListItemResource5" Text="Mobile No."></asp:ListItem>
                                            <asp:ListItem Value="EMP_EMAIL" meta:resourcekey="ListItemResource6" Enabled="False" Text="E-Mail"></asp:ListItem>
                                            <asp:ListItem Value="CP_FULL_NAME" meta:resourcekey="ListItemResource7" Text="Company"></asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlCurrentTasksSymbols" runat="server" AutoPostBack="True"
                                            CssClass="textbox" Visible="False" Width="50px" meta:resourcekey="ddlCurrentTasksSymbolsResource1">
                                            <asp:ListItem Selected="True" meta:resourcekey="ListItemResource8">=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource9">&lt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource10">&gt;</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource11">&lt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource12">&gt;=</asp:ListItem>
                                            <asp:ListItem meta:resourcekey="ListItemResource13">R</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        <asp:Label id="lblCurrentFromDate" runat="server" CssClass="label" Font-Bold="True"
                                            ForeColor="White" Text="From" Visible="False" meta:resourcekey="lblCurrentFromDateResource1"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtCurrentDayTasksFromDate" runat="server" CssClass="textbox" Visible="False" meta:resourcekey="txtCurrentDayTasksFromDateResource1"></asp:TextBox><asp:Image id="Image9" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" meta:resourcekey="Image9Resource1"></asp:Image></td>
                                    <td rowspan="3">
                                        <asp:Label id="lblCurrentToDate" runat="server" CssClass="label" Font-Bold="True"
                                            ForeColor="White" Text="To " Visible="False" meta:resourcekey="lblCurrentToDateResource1"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtCurrentDayTaskSearchText" runat="server" CssClass="textbox" meta:resourcekey="txtCurrentDayTaskSearchTextResource1"></asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" meta:resourcekey="imgCurrentDayTasksToDateResource1"></asp:Image></td>
                                    <td rowspan="3">
                                        <asp:Button id="btnCurrentDayTasksGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnCurrentDayTasksGo_Click" meta:resourcekey="btnCurrentDayTasksGoResource1" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                Visible="False"></asp:Label><asp:Label id="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                    Visible="False"></asp:Label><asp:Label id="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label><asp:Label
                                id="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
                <asp:GridView ID="gvEmployeeMaster" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataSourceID="sdsEmployeeMaster" DataKeyNames="EMP_ID" OnRowDataBound="gvEmployeeMaster_RowDataBound" meta:resourcekey="gvEmployeeMasterResource1" OnDataBound="gvEmployeeMaster_DataBound">
                    <Columns>
<asp:BoundField HeaderText="Sl.No" meta:resourceKey="BoundFieldResource1"></asp:BoundField>
<asp:BoundField DataField="EMP_ID" HeaderText="Emp Id">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="EMP_ID" SortExpression="EMP_ID" HeaderText="EMPIDHidden" meta:resourceKey="BoundFieldResource2"></asp:BoundField>
<asp:TemplateField SortExpression="EMP_FIRST_NAME" HeaderText="First Name" meta:resourceKey="TemplateFieldResource1"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("EMP_FIRST_NAME") %>' ID="TextBox1" meta:resourcekey="TextBox1Resource1"></asp:TextBox>

                            
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnEmpFirstName" onclick="lbtnEmpFirstName_Click" runat="server" Text='<%# Bind("EMP_FIRST_NAME") %>' CausesValidation="False" meta:resourcekey="lbtnEmpFirstNameResource1"></asp:LinkButton> 
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
<asp:BoundField DataField="CP_FULL_NAME" HeaderText="Company" meta:resourceKey="BoundFieldResource9">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ASSIGNED_EMPID" HeaderText="A.Emp  Id"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="EMP_DET_DOT" HeaderText="DOT"></asp:BoundField>
</Columns>
                    <SelectedRowStyle BackColor="LightSteelBlue" />
                </asp:GridView>
                <asp:SqlDataSource ID="sdsEmployeeMaster" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_HR_EMPLOYEE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</selectparameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; width: 978px;">
            </td>
        </tr>
        <tr>
        
            <td colspan="4" style="text-align: center; width: 978px;">
            <asp:Panel ID="Panel1" runat="server" Width="100%">
                <table id="tblEmployeeDetails" runat="server" border="0" cellpadding="0" cellspacing="0"
                    visible="false" width="100%">
                    
                    <tr>
            <td class="profilehead" colspan="4" style="text-align: left; background-color: #99ccff;">
                Professional Details</td>
                    </tr>
                    <tr>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left;">
                        </td>
                        <td style="height: 21px; text-align: right;">
                        </td>
                        <td style="height: 21px; text-align: left">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                <asp:Label id="Label2" runat="server" Text="Department :" meta:resourcekey="Label2Resource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlDepartment" runat="server" Width="154px" meta:resourcekey="ddlDepartmentResource1">
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label3" runat="server" Text="Date Of Appointment :" Width="140px" meta:resourcekey="Label3Resource1"></asp:Label></td>
                        <td style="text-align: left;" align="left">
                            <asp:TextBox ID="txtDateOfAppointment" runat="server" meta:resourcekey="txtDateOfAppointmentResource1"></asp:TextBox>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="lblDesignation" runat="server" Text="Designation :" Width="80px" meta:resourcekey="lblDesignationResource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlDesignation" runat="server" Width="154px" meta:resourcekey="ddlDesignationResource1">
                            </asp:DropDownList>&nbsp;
                        </td>
                        <td style="text-align: right;">
                            <asp:Label id="Label26" runat="server" Text="Company :" meta:resourcekey="Label26Resource1"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList id="ddlCompany" runat="server" meta:resourcekey="ddlCompanyResource1">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 22px;">
                            <asp:Label id="Label35" runat="server" Text="Assigned Employee Id :"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:TextBox id="txtAssignedEmpId" runat="server" meta:resourcekey="txtEmailResource1">
                        </asp:TextBox>
                        </td>
                        <td align="right" style="text-align: right; height: 22px;">
                            <asp:Label id="Label20" runat="server" Text="Assigned Mobile No :"></asp:Label></td>
                        <td style="text-align: left; height: 22px;">
                            <asp:TextBox ID="txtAssignedMobile" runat="server" meta:resourcekey="txtMobileNoResource1">
                        </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
            <td colspan="4" style="text-align: left; background-color: #99ccff;" class="profilehead">
                Employee Details</td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
            </td>
            <td style="text-align: left;">
            </td>
            <td rowspan="1" style="text-align: right;">
            </td>
            <td rowspan="1" style="text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label7" runat="server" Text="First Name :" Width="81px" meta:resourcekey="Label7Resource1"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtFirstName" runat="server" meta:resourcekey="txtFirstNameResource1"></asp:TextBox>&nbsp;
            </td>
            <td rowspan="1" style="text-align: right;">
                <asp:Label id="lblEmployeeName" runat="server" Text="Middle Name :" Width="96px" meta:resourcekey="lblEmployeeNameResource1"></asp:Label></td>
            <td rowspan="1" style="text-align: left">
                <asp:TextBox id="txtMiddleName" runat="server" meta:resourcekey="txtMiddleNameResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label6" runat="server" Text="Last Name :" Width="82px" meta:resourcekey="Label6Resource1"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox id="txtLastName" runat="server" meta:resourcekey="txtLastNameResource1"></asp:TextBox>&nbsp;
            </td>
            <td rowspan="1" style="text-align: right;">
                <asp:Label ID="Label8" runat="server" Text="Gender :" Width="59px" meta:resourcekey="Label8Resource1"></asp:Label></td>
            <td rowspan="1" style="text-align: left">
                <asp:RadioButton ID="rbtMale" runat="server" GroupName="gender" Text="Male" Checked="True" meta:resourcekey="rbtMaleResource1" /><asp:RadioButton ID="rbtFemale" runat="server" GroupName="gender" Text="Female" meta:resourcekey="rbtFemaleResource1" /></td>
                    </tr>
                    <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label10" runat="server" Text="Date Of Birth :" Width="90px" meta:resourcekey="Label10Resource1"></asp:Label></td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtDateOfBirth" runat="server" meta:resourcekey="txtDateOfBirthResource1"></asp:TextBox>
                &nbsp;
                </td>
            <td rowspan="3" style="text-align: right;">
                <asp:Label id="Label40" runat="server" Text="Image :"></asp:Label></td>
            <td rowspan="3" style="text-align: left">
                <asp:Image id="Image1" runat="server" Height="120px" ImageUrl="~/Images/noimage400x300.gif"
                    Width="147px">
                </asp:Image></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label id="Label15" runat="server" Text="Father/Husband Name :" Width="149px"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:TextBox id="txtfathername" runat="server">
                            </asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                <asp:Label ID="Label11" runat="server" Text="Email :" Width="41px" meta:resourcekey="Label11Resource1"></asp:Label></td>
                        <td style="text-align: left">
                <asp:TextBox id="txtEmail" runat="server" meta:resourcekey="txtEmailResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="2" style="height: 19px; text-align: left; background-color: #99ccff;">
                            Address for correspondence</td>
                        <td class="profilehead" colspan="2" style="height: 19px; text-align: left; background-color: #99ccff;">
                            Permanent address</td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                <asp:Label id="lblLocalAddress" runat="server" Text="Address :" Width="81px" meta:resourcekey="lblLocalAddressResource1"></asp:Label></td>
                        <td style="text-align: left;">
                <asp:TextBox id="txtAddress" runat="server" TextMode="MultiLine" meta:resourcekey="txtAddressResource1" Height="47px"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label id="Label12" runat="server" Text="Address :"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCity" runat="server" meta:resourcekey="txtCityResource1" Height="54px" TextMode="MultiLine"></asp:TextBox>
                </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"><asp:Label ID="lblpno" runat="server" Text="Phone No :" Width="72px" meta:resourcekey="lblpnoResource1">
                        </asp:Label></td>
                        <td style="text-align: left;">
                <asp:TextBox ID="txtPhoneNo" runat="server" meta:resourcekey="txtPhoneNoResource1"></asp:TextBox>
                        </td>
                        <td style="text-align: right;"><asp:Label ID="Label32" runat="server" Text="Phone No :" Width="72px" meta:resourcekey="lblpnoResource1">
                        </asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPerPhoneNo" runat="server" meta:resourcekey="txtPhoneNoResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;"><asp:Label ID="Label9" runat="server" Text="Mobile No :" Width="81px" meta:resourcekey="Label9Resource1">
                        </asp:Label></td>
                        <td style="text-align: left;">
                <asp:TextBox ID="txtMobileNo" runat="server" meta:resourcekey="txtMobileNoResource1"></asp:TextBox>
                        </td>
                        <td style="text-align: right;"><asp:Label ID="Label34" runat="server" Text="Mobile No :" Width="81px" meta:resourcekey="Label9Resource1">
                        </asp:Label></td>
                        <td style="text-align: left"><asp:TextBox ID="txtPerMobileNo" runat="server" meta:resourcekey="txtMobileNoResource1"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="profilehead" colspan="4" style="text-align: left; background-color: #99ccff;">
                            Documents Submitted</td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <table width="100%">
                                <tr>
                                    <td colspan="4" style="text-align: center; width: 852px;">
                                        <asp:CheckBoxList id="chklDocumentsSubmitted" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                            Width="100%">
                                            <asp:ListItem>VoterId Card</asp:ListItem>
                                            <asp:ListItem>Driving License</asp:ListItem>
                                            <asp:ListItem>Ration Card</asp:ListItem>
                                            <asp:ListItem>PassPort</asp:ListItem>
                                            <asp:ListItem>Pan</asp:ListItem>
                                            <asp:ListItem>Electric City</asp:ListItem>
                                            <asp:ListItem>Telephone Bill</asp:ListItem>
                                            <asp:ListItem>Bank Pass Book</asp:ListItem>
                                        </asp:CheckBoxList>
                                        <asp:Label id="lblchkllist" runat="server" Text="Please Check atleast 1 documents"
                                            Visible="False" Font-Bold="True" ForeColor="Red" EnableTheming="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 852px; text-align: center">
                                        <asp:CheckBox id="chkothers" runat="server" AutoPostBack="True" OnCheckedChanged="chkothers_CheckedChanged"
                                            Text="Others">
                                        </asp:CheckBox>
                                        <asp:TextBox id="txtOthers" runat="server" Visible="False">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Print" /></td>
                    </tr>
                </table>
                </asp:Panel>
                <table id="TABLE1" runat="server" visible="false">
                    <tr>
                        <td style="width: 100px">
                            <asp:Label ID="Label4" runat="server" Text="Date Of Termination :" Width="138px" meta:resourcekey="Label4Resource1"></asp:Label><asp:TextBox ID="txtDateOfTermination" runat="server" meta:resourcekey="txtDateOfTerminationResource1"></asp:TextBox><asp:Image id="imgDot" runat="server" ImageUrl="~/Images/Calendar.png" meta:resourcekey="imgDotResource1">
                </asp:Image><cc1:CalendarExtender  Format="dd/MM/yyyy" ID="CalendarExtender1" runat="server" Enabled="True" 
                    PopupButtonID="imgDot" TargetControlID="txtDateOfTermination">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" DisplayMoney="Left"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateOfTermination" UserDateFormat="MonthDayYear" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                            </cc1:MaskedEditExtender>
                            <asp:Label ID="Label5" runat="server" Text="Employee Type :" Width="107px" meta:resourcekey="Label5Resource1"></asp:Label><asp:DropDownList id="ddlEmployeeType" runat="server" Width="154px" meta:resourcekey="ddlEmployeeTypeResource1">
                            </asp:DropDownList><asp:Label id="Label13" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label13Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEmployeeType"
                                ErrorMessage="Please Select Employee Type" InitialValue="0" meta:resourcekey="RequiredFieldValidator3Resource1">*</asp:RequiredFieldValidator><asp:Label id="Label21" runat="server" Text="Service Bond"></asp:Label><asp:RadioButton id="rdbYes" runat="server" Text="Yes" AutoPostBack="True" GroupName="ab" OnCheckedChanged="RadioButton1_CheckedChanged">
                            </asp:RadioButton><asp:RadioButton id="rdbno" runat="server" Text="No" AutoPostBack="True" Checked="True" GroupName="ab" OnCheckedChanged="rdbno_CheckedChanged">
                            </asp:RadioButton><asp:Label id="lblYears" runat="server" Text="Years" Visible="False"></asp:Label><asp:TextBox id="txtYears" runat="server" Visible="False"></asp:TextBox><asp:Label id="Label23" runat="server" Text="Assigned Email Id :"></asp:Label><asp:TextBox id="txtAssignedEmail" runat="server" meta:resourcekey="txtEmailResource1">
                        </asp:TextBox><asp:Label id="Label33" runat="server" Text="Assigned Branch :"></asp:Label><asp:DropDownList id="ddlBranch" runat="server" Width="154px" meta:resourcekey="ddlBranchResource1">
                            </asp:DropDownList><asp:Label id="Label16" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False" meta:resourcekey="Label16Resource1"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="ddlBranch" ErrorMessage="Please Select Branch" InitialValue="0" Enabled="False" meta:resourcekey="RequiredFieldValidator5Resource1">*</asp:RequiredFieldValidator><asp:Label id="Label36" runat="server" Text="Assigned Acc No :"></asp:Label><asp:Label id="Label39" runat="server" Text="Status :"></asp:Label><asp:TextBox id="txtAssignedAccNo" runat="server" meta:resourcekey="txtEmailResource1">
                            </asp:TextBox><asp:RadioButton id="rdbActive" runat="server" Text="Active" GroupName="abc" OnCheckedChanged="RadioButton1_CheckedChanged" Checked="True">
                            </asp:RadioButton><asp:RadioButton id="rdbInactive" runat="server" Text="Inactive" GroupName="abc" OnCheckedChanged="rdbno_CheckedChanged">
                            </asp:RadioButton><asp:Label id="Label37" runat="server" Text="Insurance Info :"></asp:Label><asp:TextBox id="txtInsurance" runat="server" TextMode="MultiLine" meta:resourcekey="txtAddressResource1" Height="47px">
                            </asp:TextBox></td>
                        <td style="width: 100px">
                                        <asp:Label id="Label27" runat="server" Text="Name"></asp:Label><asp:TextBox id="txtemrName" runat="server"></asp:TextBox><asp:Label id="Label30" runat="server" Text="Address"></asp:Label><asp:TextBox id="txtemrAddress" runat="server" TextMode="MultiLine" Height="69px" EnableTheming="True" Width="248px"></asp:TextBox><asp:Label id="Label28" runat="server" Text="Relationship"></asp:Label><asp:TextBox id="txtemrRelationship" runat="server"></asp:TextBox><asp:Label id="Label29" runat="server" Text="Phone"></asp:Label><asp:TextBox id="txtemrPhone" runat="server"></asp:TextBox><asp:Label ID="Label25" runat="server" Text="User Name :" Width="81px" meta:resourcekey="Label25Resource1"></asp:Label><asp:TextBox ID="txtUserName" runat="server" meta:resourcekey="txtUserNameResource1"></asp:TextBox><asp:Label id="Label24" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*" meta:resourcekey="Label24Resource1"></asp:Label><asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Please Enter User Name">*</asp:RequiredFieldValidator></td>
                        <td style="width: 100px">
                <table id="Table2" style="width: 1px; height: 1px">
                    <tr>
                        <td>
                <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" CausesValidation="False" Text="Refresh" OnClick="btnRefresh_Click" meta:resourcekey="btnRefreshResource1" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" CausesValidation="False" Text="Close" OnClick="btnClose_Click" meta:resourcekey="btnCloseResource1" /></td>
                    </tr>
                </table>
                            <table id="Table3">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnNew" runat="server" CausesValidation="False" meta:resourcekey="btnNewResource1"
                                            OnClick="btnNew_Click" Text="New" /></td>
                                    <td>
                                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" meta:resourcekey="btnEditResource1"
                                            OnClick="btnEdit_Click" Text="Edit" /></td>
                                    <td style="width: 58px">
                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="False" meta:resourcekey="btnDeleteResource1"
                                            OnClick="btnDelete_Click" Text="Delete" /></td>
                                    <td style="width: 58px">
                                        <asp:Button ID="btnImage" runat="server" CausesValidation="False" OnClientClick="window.open('../Masters/EmpImage.aspx','resume','resizable=yes,scrool=yes,width=900,height=600,status=yes,toolbar=no,menubar=no,scrollbars=yes')"
                                            Text="Add Image" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   
                </table>
            </td>
           
        </tr>
      
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
        ShowMessageBox="True" ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1" />
            

    </form>
</body>
</html>
