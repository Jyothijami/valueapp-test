<%@ Page Title="" Language="C#" MasterPageFile="~/MPs/SalesMP1.master" AutoEventWireup="true" CodeFile="SalesEnquiryDetails.aspx.cs" Inherits="Modules_SM_SalesEnquiryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td style="text-align: left;">Sales Lead Details </td>
        </tr>
    </table>
    <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
        visible="false" width="100%">
        <tr>
            <td></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">Assign Task No

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">Enquiry No

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryNoForAssign" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right">&nbsp;Enquiry Date

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEnquiryDateForAssign" runat="server" type="datepic" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">Customer Name

            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
            <td style="text-align: right;">Contact E-Mail

            </td>
            <td style="text-align: left;">
                <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">Employee Name

            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlEmpNameForAssign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpNameForAssign_SelectedIndexChanged">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">EDP Department</asp:ListItem>
                </asp:DropDownList>*
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlEmpNameForAssign"
                    ErrorMessage="Please Select the Employee Name" InitialValue="0" ValidationGroup=" assign">*</asp:RequiredFieldValidator></td>
            <td style="text-align: right">Employee EMail

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtEmpEmailId" runat="server" ReadOnly="True"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="text-align: right">Remarks

            </td>
            <td colspan="3" style="text-align: left">
                <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False"
                    TextMode="MultiLine" Width="83%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td style="text-align: right">Assign Date

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtAssignDate" runat="server" Width="130px" type="datepic"></asp:TextBox>*
               
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                    ErrorMessage="Please Select Assign Date" ValidationGroup="assign">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="DateCustomValidate"
                        ControlToValidate="txtAssignDate" ErrorMessage="Please Enter the Assign Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                        SetFocusOnError="True" ValidationGroup="assign">*</asp:CustomValidator>

            </td>
            <td style="text-align: right">Due Date

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDueDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"
                    Width="130px"></asp:TextBox>*
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate"
                    ErrorMessage="Please Select Due Date" ValidationGroup="assign">*</asp:RequiredFieldValidator><asp:CompareValidator
                        ID="CompareValidator1" runat="server" ControlToCompare="txtAssignDate" ControlToValidate="txtDueDate"
                        ErrorMessage="Due Date should not be less than Assign Date" Operator="LessThanEqual"
                        SetFocusOnError="True" Type="Date" ValidationGroup="assign" Enabled="False">*</asp:CompareValidator><asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="DateCustomValidate"
                            ControlToValidate="txtDueDate" ErrorMessage="Please Enter the Due Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                            SetFocusOnError="True" ValidationGroup="assign">*</asp:CustomValidator>

            </td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
                <table id="Table2" align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnAssignTask" runat="server" OnClick="btnAssignTask_Click" Text="Assign"
                                ValidationGroup="assign" /></td>
                        <td>
                            <asp:Button ID="btnCloseAssignTask" runat="server" OnClick="btnCancelTask_Click"
                                Text="Close" CausesValidation="False" /></td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="assign" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
            <td style="text-align: right"></td>
            <td style="text-align: left"></td>
        </tr>
    </table>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">

        <tr>
            <td colspan="4" style="height: 19px; text-align: center">
                <table border="0" cellpadding="0" cellspacing="0" id="tblSalesEnquiry" runat="server"
                    visible="false" width="100%">
                    <tr>
                        <td id="TD10" class="profilehead" colspan="4" style="text-align: left">Enquiry Details</td>
                    </tr>
                    <tr>
                        <td id="TD27" style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left;"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="TD14">Enquiry No

                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                        <td style="text-align: right;">Enquiry Date

                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEnquiryDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtEnquiryDate"
                                ErrorMessage="Please Select the Enquiry Date" ValidationGroup="main">*</asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please Enter the Enquiry Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not" ClientValidationFunction="DateCustomValidate" ControlToValidate="txtEnquiryDate" SetFocusOnError="True">*</asp:CustomValidator>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="TD23">Enquiry Originated By

                        </td>
                        <td style="text-align: left;">
                            <asp:RadioButton ID="rbEmployee" runat="server" GroupName="rbtOriginatedBy" Text="Employee"
                                AutoPostBack="True" Checked="True" OnCheckedChanged="rbEmployeeAgent_CheckedChanged"></asp:RadioButton>
                            <asp:RadioButton ID="rbAgent" runat="server" GroupName="rbtOriginatedBy" Visible="false"
                                Text="Agent" AutoPostBack="True" OnCheckedChanged="rbEmployeeAgent_CheckedChanged" Enabled="False"></asp:RadioButton></td>
                        <td style="text-align: right;">Enquiry Source

                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlEnquirySource" runat="server" OnSelectedIndexChanged="ddlEnquirySource_SelectedIndexChanged">
                            </asp:DropDownList>*
                            
                            <asp:RequiredFieldValidator ID="rfvEnqSource" runat="server" ControlToValidate="ddlEnquirySource"
                                ErrorMessage="Please Select the Enquiry Source" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblOrginatedList" runat="server" Text="Employee Name"></asp:Label></td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlOriginatedBy" runat="server">
                            </asp:DropDownList>*
                            
                            <asp:RequiredFieldValidator ID="rfvOriginatedBy" runat="server" ControlToValidate="ddlOriginatedBy"
                                ErrorMessage="Please Select the Employee Name" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblReferenceCode" runat="server" Text="Reference :" Width="108px" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtReferenceCode" runat="server" Visible="False">0</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                        <td style="text-align: right">&nbsp;</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtContactNo" runat="server" Visible="False">0</asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftxtePhoneNo" runat="server" TargetControlID="txtContactNo"
                                ValidChars=" +()-0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="tdTenderDate1" runat="server" visible="false">Tender Date
                            <%--<asp:Label ID="lblDescription" runat="server" Text="Description" Width="73px"></asp:Label>--%></td>
                        <td style="text-align: left;" id="tdTenderDate2" runat="server" visible="false">
                            <asp:TextBox ID="txtTenderDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>*
                            <%--<asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTenderDate"
                                ErrorMessage="Please Select the Tender Date">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator8" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtTenderDate" ErrorMessage="Please Enter the Enquiry Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>

                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtTenderDate" UserDateFormat="MonthDayYear" ClearTextOnInvalid="True">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblEnquiryDueDate" runat="server" Text="Enquiry Due Date" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtEnquiryDueDate" runat="server" CssClass="datetext" type="date" Visible="False"></asp:TextBox>&nbsp;<%--<asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*">                           </asp:Label>--%><asp:RequiredFieldValidator ID="rfvEnquiryDueDate" runat="server" ControlToValidate="txtEnquiryDueDate"
                                ErrorMessage="Please Select the Enquiry Due Date" ValidationGroup="main">*</asp:RequiredFieldValidator><asp:CompareValidator
                                    ID="CompareValidator2" runat="server" ControlToCompare="txtCurrentDateHidden"
                                    ControlToValidate="txtEnquiryDueDate" ErrorMessage="Enquiry Due Date should not be less than Current Date"
                                    Operator="GreaterThanEqual" Type="Date" Enabled="False">*</asp:CompareValidator><asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="DateCustomValidate"
                                        ControlToValidate="txtEnquiryDueDate" ErrorMessage="Please Enter the Enquiry Due Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                        SetFocusOnError="True">*</asp:CustomValidator>
                            <asp:TextBox ID="txtCurrentDateHidden" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td id="Td1" runat="server" style="text-align: right" visible="false"></td>
                        <td id="Td2" runat="server" style="text-align: left" visible="false"></td>
                        <td style="text-align: right">
                            <%-- Priority : <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>--%></td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlenqpriority" Visible="false" runat="server">
                                <asp:ListItem Value="0">--</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td id="tdOpeningDate1" runat="server" style="text-align: right" visible="false">Opening Date
                            <%--<asp:Label ID="Label9" runat="server" Text="Industry Type"></asp:Label>--%>

                        </td>
                        <td id="tdOpeningDate2" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtOpeningDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                            <asp:CompareValidator
                                ID="CompareValidator3" runat="server" ControlToCompare="txtCurrentDateHidden"
                                ControlToValidate="txtOpeningDate" ErrorMessage="Opening Date should not be less than Current Date"
                                Operator="GreaterThanEqual" Type="Date" Enabled="False">*</asp:CompareValidator><asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="DateCustomValidate"
                                    ControlToValidate="txtOpeningDate" ErrorMessage="Please Enter the Opening Date in DD/MM/YYYY Format or Check  Year in 2000 to 2099 Range or not"
                                    SetFocusOnError="True">*</asp:CustomValidator>

                            <cc1:MaskedEditExtender ID="meeOpeningDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                                MaskType="Date" TargetControlID="txtOpeningDate" UserDateFormat="MonthDayYear">
                            </cc1:MaskedEditExtender>
                        </td>
                        <td id="tdOpeningTime1" runat="server" style="text-align: right" visible="false">Opening Time
                            <%--<asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label>--%>

                        </td>
                        <td id="tdOpeningTime2" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtOpeningTime" runat="server">
                            </asp:TextBox><asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="TimeCustomValidate"
                                ControlToValidate="txtOpeningTime" ErrorMessage="Please Enter the Opening Time in the range from 09:00 AM to 06:00 PM"
                                SetFocusOnError="True">*</asp:CustomValidator>
                            <cc1:MaskedEditExtender
                                ID="meeOpeningTime" runat="server" AcceptAMPM="True" DisplayMoney="Left" Enabled="True"
                                Mask="99:99" MaskType="Time" TargetControlID="txtOpeningTime">
                            </cc1:MaskedEditExtender>
                        </td>
                    </tr>
                    <tr>
                        <td id="tdSubmissionTime2" runat="server" style="text-align: right" visible="false">Submission Time
                            <%--<asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label>--%>

                        </td>
                        <td id="tdSubmissionTime1" runat="server" style="text-align: left" visible="false">
                            <asp:TextBox ID="txtSubmissionTime" runat="server">
                            </asp:TextBox>*
                            <%--<asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>--%>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSubmissionTime"
                                ErrorMessage="Please Enter the Submission Time">*</asp:RequiredFieldValidator><asp:CustomValidator
                                    ID="CustomValidator7" runat="server" ClientValidationFunction="TimeCustomValidate"
                                    ControlToValidate="txtSubmissionTime" ErrorMessage="Please Enter the Submission Time in the range from 09:00 AM to 06:00 PM"
                                    SetFocusOnError="True">*</asp:CustomValidator><cc1:MaskedEditExtender
                                        ID="meeSubmissionTime" runat="server" AcceptAMPM="True" DisplayMoney="Left" Enabled="True"
                                        Mask="99:99" MaskType="Time" TargetControlID="txtSubmissionTime" ClearTextOnInvalid="True">
                                    </cc1:MaskedEditExtender>
                        </td>
                        <td runat="server" style="text-align: right" visible="false"></td>
                        <td runat="server" style="text-align: left" visible="false"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblPromotionType" runat="server" Text="Promotion Type" Visible="False"></asp:Label>

                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPromotionType" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label29" runat="server" EnableTheming="False" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfvPromotionType" runat="server" ControlToValidate="txtPromotionType"
                                ErrorMessage="Please Enter the Promotion Type" Visible="False">*</asp:RequiredFieldValidator></td>
                        <td style="text-align: right">
                            <asp:Label ID="lblPromotionActivity" runat="server" Text="Promotion Activity" Visible="False"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPromotionActivity" runat="server" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblCriteria" runat="server" Text="Follow Up Criteria" Visible="False"></asp:Label></td>
                        <td style="text-align: left" colspan="3">
                            <asp:TextBox ID="txtFollowUpCriteria" runat="server" CssClass="multilinetext" EnableTheming="False"
                                TextMode="MultiLine" Width="89%" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="TD11">&nbsp;</td>
                        <td colspan="3" style="text-align: left">
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                EnableTheming="False" Width="89%" Visible="False">0</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" id="TD15"></td>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right"></td>
                        <td style="text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left" class="profilehead" id="TD16">Customer Details</td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right" id="TD5"></td>
                        <td style="height: 19px; text-align: left;"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                            <table style="width:100%">
                                 <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label22" runat="server" Text="Search"></asp:Label></td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="TextBox2" runat="server">
                                        </asp:TextBox><asp:Button ID="btngo2" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" OnClick="btngo2_Click" Text="Go" /><asp:SqlDataSource
                                                ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                SelectCommand="SP_Customer_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter PropertyName="Text" Type="String" Name="SearchValue" ControlID="TextBox2"></asp:ControlParameter>
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                    </td>
                                    <td style="text-align: right"></td>
                                    <td style="text-align: left"></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; height: 22px;" id="TD18">Customer
                            <%--<asp:Label ID="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label>--%>

                                    </td>
                                    <td style="text-align: left; height: 22px;">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label57" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomer"
                                            ErrorMessage="Please Select the Customer" InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                    <td style="text-align: right; height: 22px;">Region
                            <%--<asp:Label ID="Label36" runat="server" EnableTheming="False" ForeColor="Red"
                                Text="*"> </asp:Label>--%>

                                    </td>
                                    <td style="text-align: left; height: 22px;">
                                        <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="height: 22px; text-align: right">Industry Type
                            <%--<asp:Label ID="Label58" runat="server" Text="Search For ModelNo:" Width="140px"></asp:Label>--%>

                                    </td>
                                    <td style="height: 22px; text-align: left">
                                        <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True">
                                        </asp:TextBox></td>
                                    <td style="height: 22px; text-align: right">Unit Name
                            <%--<asp:Label ID="Label11" runat="server" Text="Model No :"></asp:Label>--%>

                                    </td>
                                    <td style="height: 22px; text-align: left">
                                        <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="Label56" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" class="auto-style1">
                                        <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label>

                                    </td>
                                    <td colspan="3" style="text-align: left" class="auto-style1">
                                        <asp:TextBox ID="txtUnitAddress" runat="server" EnableTheming="False" TextMode="MultiLine"
                                            Width="569px" Font-Names="Verdana" Font-Size="8pt" CssClass="multilinetext"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">Contact Person
                            <%--<asp:Label ID="Label4" runat="server" Text="Model Name :" Width="100px"></asp:Label>--%>

                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox><asp:DropDownList
                                            ID="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                                            Visible="False">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="Label58" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                        &nbsp;<asp:RequiredFieldValidator ID="rfvContactPerson" runat="server"
                                            ControlToValidate="ddlContactPerson" ErrorMessage="Please Select the Contact Person"
                                            InitialValue="0" ValidationGroup="main">*</asp:RequiredFieldValidator></td>
                                    <td style="text-align: right">Email
                            <%--<asp:Label ID="Label32" runat="server" Text="Item Category :"></asp:Label>--%>

                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" id="TD28">Phone No
                            <%--<asp:Label ID="Label33" runat="server" Text="Item SubCategory :"></asp:Label>--%>

                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True">
                                        </asp:TextBox></td>
                                    <td style="text-align: right">Mobile
                            <%--<asp:Label ID="Label54" runat="server" Text="Color :"></asp:Label>--%>

                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True">
                                        </asp:TextBox></td>
                                </tr>
                            </table>
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>


                    <tr>
                        <td colspan="4" style="text-align: left; height: 20px;" class="profilehead" id="TD19">Interested Product</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                        <td style="text-align: right; height: 19px;"></td>
                        <td style="text-align: left; height: 19px;"></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                            <table>
                                <tr>
                                    <td colspan="4">
                                        <tr>
                                            <td colspan="4">
                                                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>--%>
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: right">Search For ModelNo:
                            
                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtSearchModel" runat="server">
                                                                    </asp:TextBox><asp:Button ID="btnSearchModelNo" runat="server" BorderStyle="None"
                                                                        CausesValidation="False" CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click"
                                                                        Text="Go" /><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                                            SelectCommand="SP_MODELNO_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                                                            <SelectParameters>
                                                                                <asp:Parameter Type="Int64" DefaultValue="0" Name="BranbId"></asp:Parameter>
                                                                                <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="txtSearchModel"></asp:ControlParameter>
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                </td>
                                                                <td colspan="2"  style="text-align:left">
                                                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="Select The Brand Only if U R NOT Searching the Model No..!"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">&nbsp;Brand :
                            <%--<asp:Label ID="Label5" runat="server" Text="UOM :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left; margin-left: 40px;">
                                                                    
                                                                    <asp:DropDownList ID="ddlBrandStock" runat="server" OnSelectedIndexChanged="ddlBrandStock_SelectedIndexChanged"
                                                                        Width="149px" AutoPostBack="True" ValidateRequestMode="Disabled">
                                                                    </asp:DropDownList>&nbsp; <b>OR</b> &nbsp;
                                    <asp:TextBox ID="txtBrandName" runat="server" AutoPostBack="True" OnTextChanged="txtBrandName_TextChanged"></asp:TextBox>
                                                                    

                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;">Model No :
                            <%--<asp:Label ID="Label3" runat="server" Text="Item Specification :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>--%>
                                                                    <asp:DropDownList ID="ddlModelNo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModelNo_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <b>OR</b>
                                                                    <asp:TextBox ID="txtModelNo" runat="server" OnTextChanged="txtModelNo_TextChanged"></asp:TextBox>
                                                                    <asp:Label ID="Label59" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Please Enter/select Model Number" ControlToValidate="txtModelNo" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                                                    <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>


                                                                </td>
                                                                <td style="text-align: right;">Item Name :
                            <%--<asp:Label ID="Label7" runat="server" Text="Specifications :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtItemName" runat="server">
                                                                    </asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right; height: 24px;">Item Category :
                            <%--<asp:Label ID="Label10" runat="server" Text="Remarks :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left; height: 24px;">
                                                                    <asp:TextBox ID="txtItemCategory" runat="server"></asp:TextBox>
                                                                    <asp:Label ID="Label60" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Item Category" ControlToValidate="txtItemCategory" ValidationGroup="ip">*</asp:RequiredFieldValidator>

                                                                </td>
                                                                <td style="text-align: right; height: 24px;">Item SubCategory :
                            <%--<asp:Label ID="Label56" runat="server" Text="Room :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left; height: 24px;">
                                                                    <asp:TextBox ID="txtItemSubCategory" runat="server"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">Color :
                            <%--<asp:Label ID="Label41" runat="server" Text="Doc. Charges :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlcolor" runat="server" OnSelectedIndexChanged="ddlcolor_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                                    <b>OR</b>
                                                                    <asp:TextBox ID="txtcolorName" runat="server"></asp:TextBox>
                                                                    <asp:Label ID="Label61" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Please Enter/select Colour" ControlToValidate="txtcolorName" ValidationGroup="ip">*</asp:RequiredFieldValidator>

                                                                </td>
                                                                <td style="text-align: right">
                                                                    <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">UOM :
                            <%--<asp:Label ID="Label48" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtItemUOM" runat="server"></asp:TextBox></td>
                                                                <td style="text-align: right">Quantity :
                            <%--<asp:Label ID="Label44" runat="server" Text="In Favour of :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtItemQuantity" runat="server">
                                                                    </asp:TextBox>
                                                                    <asp:Label ID="Label62" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtItemQuantity"
                                                                        ErrorMessage="Please Enter the Quantity" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">Item Specification :
                            <%--<asp:Label ID="Label42" runat="server" Text="EMD Charges :"></asp:Label>--%>

                                                                </td>
                                                                <td colspan="3" style="text-align: left">
                                                                    <asp:TextBox ID="txtItemSpec" runat="server" CssClass="multilinetext" EnableTheming="False" TextMode="MultiLine" Width="89%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">Specifications :
                            <%--<asp:Label ID="Label50" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left" colspan="3">
                                                                    <asp:TextBox ID="txtItemSpecifications" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                                                        EnableTheming="False" Width="89%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right">Remarks :
                            <%--<asp:Label ID="Label45" runat="server" Text="In Favour of :"></asp:Label>--%>

                                                                </td>
                                                                <td colspan="3" style="text-align: left">
                                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="multilinetext"
                                                                        EnableTheming="False" Width="89%"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px; text-align: right">
                                                                    <asp:Label ID="lblPriority" runat="server" Text="Priority :" Visible="False"></asp:Label>
                                                                </td>
                                                                <td style="height: 19px; text-align: left;">
                                                                    <asp:DropDownList ID="ddlPriority" runat="server" Visible="False" AutoPostBack="True">
                                                                        <asp:ListItem Value="0">--</asp:ListItem>
                                                                        <asp:ListItem Value="1">Low</asp:ListItem>
                                                                        <asp:ListItem Value="2">Medium</asp:ListItem>
                                                                        <asp:ListItem Value="3">High</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                                <td style="height: 19px; text-align: right">Room :
                            <%--<asp:Label ID="Label51" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>--%>

                                                                </td>
                                                                <td style="height: 19px; text-align: left">
                                                                    <asp:TextBox ID="txtRoom" runat="server">
                                                                    </asp:TextBox>
                                                                    <asp:Label ID="Label63" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
                                                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtRoom"
                                                                        ErrorMessage="Please Enter the Room" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" id="tdDocCharges1" runat="server" visible="false">Doc. Charges :
                            <%--<asp:Label ID="Label52" runat="server" Text="Item Image :"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left" id="tdDocCharges2" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtDocCharges" runat="server">0</asp:TextBox>*
                            <%--<asp:Label ID="Label57" runat="server" Text="Quantity In Hand"></asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDocCharges"
                                                                        ErrorMessage="Please Enter the Doc Charges" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="ftxtDOcCharges" runat="server" TargetControlID="txtDocCharges"
                                                                        ValidChars=".0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="text-align: right" id="tdInfavourOfDoc1" runat="server" visible="false">In Favour of :
                            <%--<asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left" id="tdInfavourOfDoc2" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtInFavourofDoc" runat="server">0</asp:TextBox>*
                            <%--<asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtInFavourofDoc"
                                                                        ErrorMessage="Please Enter the Infavour of Doc" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right" id="tdEMDCharges1" runat="server" visible="false">EMD Charges :
                            <%--<asp:Label ID="Label1" runat="server" Text="Delivery Date"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left" id="tdEMDCharges2" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtEMDCharges" runat="server">0</asp:TextBox>*
                            <%--<asp:Label
                        ID="Label37" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                    </asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEMDCharges"
                                                                        ErrorMessage="Please Enter the EMD Charges" ValidationGroup="ip">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="ftxtEMDCharges" runat="server" TargetControlID="txtEMDCharges"
                                                                        ValidChars=".0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="text-align: right" id="tdInfavourOfEMD1" runat="server" visible="false">In Favour of :
                            <%--<asp:Label ID="Label2" runat="server" Text="Delivery Type"></asp:Label>--%>

                                                                </td>
                                                                <td style="text-align: left" id="tdInfavourOfEMD2" runat="server" visible="false">
                                                                    <asp:TextBox ID="txtInFavourofEMD" runat="server">0</asp:TextBox>*
                            <%--<asp:Label ID="Label38" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtInFavourofEMD"
                                                                        ErrorMessage="Please Enter the Infavour of EMD" ValidationGroup="ip">*</asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 19px; text-align: right">Item Image :
                            <%--<asp:Label ID="Label52" runat="server" Text="Item Image :"></asp:Label>--%>

                                                                </td>
                                                                <td style="height: 19px; text-align: left;">
                                                                    <asp:Image ID="Image1" runat="server" Height="85px" ImageUrl="~/Images/noimage400x300.gif"
                                                                        Width="140px"></asp:Image></td>
                                                                <td style="height: 19px; text-align: right"></td>
                                                                <td style="height: 19px; text-align: left">
                                                                    <asp:TextBox ID="txtQuantityInHand" runat="server" Visible="False"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right"></td>
                                            <td style="text-align: right;">
                                                <asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                                    ValidationGroup="ip" /></td>
                                            <td style="text-align: left">
                                                <asp:Button ID="btnRefreshItems" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    CssClass="loginbutton" EnableTheming="False" Text="Refresh" CausesValidation="False"
                                                    OnClick="btnRefreshItems_Click" /></td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 34px; text-align: right"></td>
                                            <td style="height: 34px; text-align: left;"></td>
                                            <td style="height: 34px; text-align: right"></td>
                                            <td style="height: 34px; text-align: left"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center" colspan="4" id="TD12" runat="server">

                                                <asp:GridView ID="gvInterestedProducts" runat="server" AutoGenerateColumns="False"
                                                    OnRowDataBound="gvInterestedProducts_RowDataBound" OnRowDeleting="gvInterestedProducts_RowDeleting"
                                                    OnRowEditing="gvInterestedProducts_RowEditing" OnSelectedIndexChanged="gvInterestedProducts_SelectedIndexChanged" Width="100%">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="true"></asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                                                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ModelNo" HeaderText="Model No">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Brand" HeaderText="Brand">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>

                                                            <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Specifications" NullDisplayText="-" HeaderText="Specifications"></asp:BoundField>
                                                        <asp:BoundField DataField="Remarks" NullDisplayText="-" HeaderText="Remarks"></asp:BoundField>
                                                        <asp:BoundField DataField="Priority" HeaderText="Priority"></asp:BoundField>
                                                        <asp:BoundField DataField="DocCharges" NullDisplayText="-" HeaderText="Doc Charges"></asp:BoundField>
                                                        <asp:BoundField DataField="DocInFavourOf" NullDisplayText="-" HeaderText="Doc In Favour Of"></asp:BoundField>
                                                        <asp:BoundField DataField="EMDCharges" NullDisplayText="-" HeaderText="EMD Charges"></asp:BoundField>
                                                        <asp:BoundField DataField="EMDInFavourOf" NullDisplayText="-" HeaderText="EMD In Favour Of"></asp:BoundField>
                                                        <asp:BoundField DataField="Room" HeaderText="Room"></asp:BoundField>
                                                        <asp:BoundField DataField="Color" HeaderText="Color "></asp:BoundField>
                                                        <asp:BoundField DataField="ColorId" HeaderText="ColorId"></asp:BoundField>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" ></asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </td>
                                </tr>
                            </table>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 19px" id="TD26" class="profilehead" colspan="4">Reference Details</td>
                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="auto-style3">Prepared By
                            <%--<asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" class="auto-style3">
                            <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                        <td style="text-align: right" class="auto-style3">Approved By
                            <%--<asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"></asp:Label>--%>

                        </td>
                        <td style="text-align: left" class="auto-style3">
                            <asp:DropDownList ID="ddlApprovedBy" runat="server" Enabled="False">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                        <td style="height: 19px; text-align: right"></td>
                        <td style="height: 19px; text-align: left"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 49px" id="TD37">
                            <table id="tblButtons" align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign"
                                            CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="main" /></td>
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" CausesValidation="False" /></td>
                                    <td>
                                        <asp:Button ID="btnRegret" runat="server" Text="Obsolete" CausesValidation="False"
                                            OnClick="btnRegret_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CausesValidation="False"
                                            OnClick="btnRefresh_Click" /></td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" OnClick="btnClose_Click" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                                            OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px" id="TD8"></td>
                        <td style="height: 21px;"></td>
                        <td style="height: 21px;"></td>
                        <td style="height: 21px"></td>
                    </tr>
                </table>
                &nbsp;
                <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" PopupControlID="Panel1"
                    RepositionMode="None" TargetControlID="btnForApproveHidden">
                </cc1:ModalPopupExtender>
                <asp:Button ID="btnForApproveHidden" runat="server" CausesValidation="False" Text="for approve hidden" /></td>
        </tr>
    </table>

    <table border="0" cellpadding="0" cellspacing="0" id="Table3" runat="server" visible="false"
        width="100%">
        <tr>
            <td style="text-align: right" id="TD29">Delivery Date
                <%--<asp:Label ID="Label1" runat="server" Text="Delivery Date"></asp:Label>--%>

            </td>
            <td style="text-align: left">
                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>*
                <%--<asp:Label
                        ID="Label37" runat="server" EnableTheming="False" ForeColor="Red" Text="*">
                    </asp:Label>--%>
                <asp:RequiredFieldValidator ID="rfvDeliveryDate" runat="server" ControlToValidate="txtDeliveryDate"
                    ErrorMessage="Please Select the Delivery Date" Enabled="False">*</asp:RequiredFieldValidator>

                <cc1:MaskedEditExtender ID="meeDEliveryDate" runat="server" DisplayMoney="Left" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtDeliveryDate" UserDateFormat="MonthDayYear">
                </cc1:MaskedEditExtender>
                <asp:TextBox ID="txtColor" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
            <td style="text-align: right">Delivery Type
                <%--<asp:Label ID="Label2" runat="server" Text="Delivery Type"></asp:Label>--%>

            </td>
            <td style="text-align: left">
                <asp:DropDownList ID="ddlDeliveryType" runat="server">
                </asp:DropDownList>*
                <%--<asp:Label ID="Label38" runat="server" EnableTheming="False" ForeColor="Red"
                    Text="*">
                </asp:Label>--%>
                <asp:RequiredFieldValidator ID="rfvDeliveryType" runat="server" ControlToValidate="ddlDeliveryType"
                    ErrorMessage="Please Select the Delivery Type" Enabled="False">*</asp:RequiredFieldValidator></td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>

    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="main"></asp:ValidationSummary>
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="ip" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
    <asp:SqlDataSource ID="sdsSalesEnquiry" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
        SelectCommand="SP_SM_SALESENQUIRY_SEARCH_SELECT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="CpId" ControlID="lblCPID"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="USERTYPE" ControlID="lblUserType"></asp:ControlParameter>
            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
        Visible="False"></asp:Label>
    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>

    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


 
