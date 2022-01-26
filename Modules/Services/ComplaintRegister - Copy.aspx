<%@ Page Language="C#" MasterPageFile="~/MPs/ServicesMP1.master" AutoEventWireup="true"
    CodeFile="ComplaintRegister - Copy.aspx.cs" Inherits="Modules_Services_ComplaintRegister"
    Title="|| ValueApp : Services : Complaint Register  ||" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 19px;
            width: 73px;
            text-align: right;
        }

        .auto-style3 {
            height: 38px;
        }

        .auto-style4 {
            height: 19px;
        }

        #TD18 {
            text-align: right;
        }

        #Td112 {
            text-align: left;
        }

        #Td114 {
            text-align: left;
        }
        .auto-style5 {
            height: 18px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }

        .auto-style2 {
            color: #FF0000;
        }

        .auto-style3 {
            width: 192px;
            height: 20px;
        }

        .auto-style4 {
            width: 362px;
            height: 20px;
        }

        .auto-style5 {
            width: 398px;
            height: 20px;
        }

        .auto-style6 {
            width: 332px;
            height: 20px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ShowPopupMail() {
            $('#mask1').show();
            $('#<%=pnlmail.ClientID %>').show();
        }
        function HidePopupMail() {
            $('#mask1').hide();
            $('#<%=pnlmail.ClientID %>').hide();
        }
        $(".btnCloseMail").live('click', function () {
            HidePopupMail();
        });
    </script>
    
    <script src="../../jsTimeago/jquery.min.js"></script>
    <script src="../../jsTimeago/jquery.timeago.js"></script>
    <script src="../../jsTimeago/test_helpers.js"></script>

<script type="text/javascript">
    jQuery(document).ready(function ($) {
        prepareDynamicDates();
        $("label.timeago").timeago();
    });

</script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    

            <table border="0" cellpadding="0" cellspacing="0" class="pagehead" style="width: 100%">
                <tr>
                    <td style="text-align: left">Complaint Register</td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td colspan="4" style="text-align: left" class="searchhead">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" id="TABLE3" onclick="return TABLE3_onclick()">
                            <tr>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" align="right" width="100%">
                                        <tr>
                                            <td style="text-align: left">Go To Page :
                                    <asp:TextBox ID="txtPageNo" Width="100px" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnPageNoSearch" runat="server" BorderStyle="None" CausesValidation="False" EnableTheming="false" CssClass="gobutton" Text="GO" OnClick="btnPageNoSearch_Click" />
                                            </td>

                                            <td style="height: 25px; text-align: right;">
                                                <asp:Label ID="Label112" CssClass="label" runat="server" EnableTheming="False" Font-Bold="True"
                                                    Text="Search By"></asp:Label>
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="CR_NO">CR No</asp:ListItem>
                                                    <asp:ListItem Value="CR_DATE">CR Date</asp:ListItem>
                                                    <asp:ListItem Value="CR_CALL_TYPE">Call Type</asp:ListItem>
                                                    <asp:ListItem Value="Cust_Name">Customer</asp:ListItem>
                                                    <asp:ListItem Value="Cust_Contact_Person">Contact Person</asp:ListItem>
                                                    <asp:ListItem Value="CR_STATUS">Status</asp:ListItem>
                                                    <asp:ListItem Value="Contact_Mobile">Unit Mobile</asp:ListItem>
                                                    <asp:ListItem Value="EMP_FIRST_NAME" >Prepared By</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                                    EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                                    Visible="False" Width="50px">
                                                    <asp:ListItem Selected="True">=</asp:ListItem>
                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                    <asp:ListItem>&lt;=</asp:ListItem>
                                                    <asp:ListItem>&gt;=</asp:ListItem>
                                                    <asp:ListItem>R</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtSearchValueFromDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                                    Width="106px"></asp:TextBox>
                                                <%-- <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False"></asp:Image>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchFrom" runat="server" Enabled="False"
                                        PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" Enabled="False"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate" UserDateFormat="MonthDayYear"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="">
                                    </cc1:MaskedEditExtender>--%>
                                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False"></asp:Label>
                                                <asp:TextBox ID="txtSearchValueToDate" runat="server" type="datepic" EnableTheming="True" Visible="False"
                                                    Width="106px"></asp:TextBox>
                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox>
                                                <%--<asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False"></asp:Image>
                                    <cc1:CalendarExtender Format="dd/MM/yyyy" ID="ceSearchValueToDate" runat="server"
                                        Enabled="False" PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" Enabled="False"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText" UserDateFormat="MonthDayYear"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="">
                                    </cc1:MaskedEditExtender>--%>
                                                <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                        Visible="False"></asp:Label><asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                                        Visible="False"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:GridView ID="gvComplaintRegister" runat="server" AutoGenerateColumns="False" 
                            AllowSorting="True" DataSourceID="sdsComplaintRegister" Width="100%" 
                            SelectedRowStyle-BackColor="#c0c0c0" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="CR_ID" HeaderText="Complaint Id" SortExpression="CR_ID" />
                                <asp:TemplateField HeaderText="Complaint Number" SortExpression="CR_NO">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CR_NO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnCRNo" runat="server" ForeColor="#0066ff" OnClick="lbtnCRNo_Click" Text="<%# Bind('CR_NO') %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cust_Name" HeaderText="Customer Name" SortExpression="Cust_Name" />
                                <asp:BoundField DataField="Cust_Contact_Person" HeaderText="Contact Person" SortExpression="Cust_Contact_Person" />
                                <asp:BoundField DataField="CR_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date" HtmlEncode="False" SortExpression="CR_DATE" />
                                <asp:BoundField DataField="CR_CALL_TYPE" HeaderText="Call Type" SortExpression="CR_CALL_TYPE" />
                                <asp:BoundField DataField="CR_STATUS" HeaderText="Status" SortExpression="CR_STATUS" />
                                <asp:BoundField DataField="TotalTime" HeaderText="TotalTime" SortExpression="TotalTime" />

                                <asp:BoundField DataField="PREPAREDBY" HeaderText="Prepared By" SortExpression="PREPAREDBY" />
                                <asp:BoundField DataField="Cust_Mobile" HeaderText="Cust Contact" SortExpression="Cust_Mobile" />
                                <asp:BoundField DataField="Contact_Mobile" HeaderText="Unit Contact" SortExpression="Contact_Mobile" />

                            </Columns>
                        </asp:GridView>
                        <%-- <asp:SqlDataSource ID="sdsComplaintRegister" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_COMPLAINT_REGISTER_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>--%>

                        <asp:SqlDataSource ID="sdsComplaintRegister" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_COMPLAINT_REGISTER_SEARCH_SELECT1" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom" PropertyName="Text" Type="String" />
                            <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="UserType" ControlID="lblUserType"></asp:ControlParameter>
                                <asp:ControlParameter PropertyName="Text" Type="Int64" DefaultValue="0" Name="EmpId" ControlID="lblEmpIdHidden"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <table id="Table1">
                            <tr>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" CausesValidation="False" OnClick="btnNew_Click"
                                        Text="New" /></td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" CausesValidation="False" OnClick="btnEdit_Click"
                                        Text="Edit" /></td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" OnClick="btnDelete_Click"
                                        Text="Delete" /></td>
                                <td style="width: 3px">
                                    <asp:Button ID="btnAssign" runat="server" CausesValidation="False" OnClick="btnAssign_Click"
                                        Text="Assign" Visible="False" /></td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td colspan="4">
                       <%-- <asp:UpdatePanel runat="server">
                            <ContentTemplate>--%>
                                <table>
                            <tr>
                                <td colspan="4">
                                    <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                                        visible="False" width="100%">
                                        <tr id="Tr1" runat="server">
                                            <td id="Td1"></td>
                                            <td id="Td2" style="text-align: left"></td>
                                            <td id="Td3" style="text-align: right"></td>
                                            <td id="Td4" style="text-align: left"></td>
                                        </tr>
                                        <tr id="Tr2" runat="server">
                                            <td id="Td151" style="text-align: right; height: 24px;">
                                                <asp:Label ID="Label19" runat="server" Font-Bold="False" Text="Assign Task No"></asp:Label></td>
                                            <td id="Td6" style="text-align: left; height: 24px;">
                                                <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td id="Td7" style="text-align: right; height: 24px;"></td>
                                            <td id="Td8" style="text-align: left; height: 24px;"></td>
                                        </tr>
                                        <tr id="Tr3" runat="server">
                                            <td id="Td9" style="text-align: right">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Text="Register No"></asp:Label></td>
                                            <td id="Td10" style="text-align: left">
                                                <asp:TextBox ID="txtEnquiryNoForAssign" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td id="Td11" style="text-align: right">&nbsp;<asp:Label ID="Label13" runat="server" Font-Bold="False" Text="Register Date"></asp:Label></td>
                                            <td id="Td12" style="text-align: left">
                                                <asp:TextBox ID="txtEnquiryDateForAssign" runat="server" ReadOnly="True"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="meeEnquiryDateForAssing" runat="server" Mask="99/99/9999"
                                                    MaskType="Date" TargetControlID="txtEnquiryDateForAssign" UserDateFormat="MonthDayYear"
                                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True">
                                                </cc1:MaskedEditExtender>
                                            </td>
                                        </tr>
                                        <tr id="Tr4" runat="server">
                                            <td id="Td13" style="text-align: right">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="False" Text="Customer Name"></asp:Label></td>
                                            <td id="Td14" style="text-align: left">
                                                <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td id="Td15" style="text-align: right">
                                                <asp:Label ID="Label16" runat="server" Font-Bold="False" Text="Contact E-Mail"></asp:Label></td>
                                            <td id="Td16" style="text-align: left">
                                                <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr id="Tr5" runat="server">
                                            <td id="Td17" style="text-align: right">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="False" Text="Employee Name"></asp:Label></td>
                                            <td id="Td118" style="text-align: left">
                                                <asp:DropDownList ID="ddlEmpNameForAssign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpNameForAssign_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:Label ID="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlEmpNameForAssign"
                                                    ErrorMessage="Please Select the Employee Name" InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                                            <td id="Td19" style="text-align: right">
                                                <asp:Label ID="Label18" runat="server" Font-Bold="False" Text="Employee EMail"></asp:Label></td>
                                            <td id="Td20" style="text-align: left">
                                                <asp:TextBox ID="txtEmpEmailId" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr id="Tr6" runat="server">
                                            <td id="Td21" style="height: 52px; text-align: right">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="False" Text="Remarks"></asp:Label></td>
                                            <td id="Td22" colspan="3" style="height: 52px; text-align: left">
                                                <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    TextMode="MultiLine" Width="83%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                                        runat="server" ControlToValidate="txtRemarksForAssingn" ErrorMessage="Please Enter Remarks"
                                                        ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr id="Tr7" runat="server">
                                            <td id="Td23" style="text-align: right"></td>
                                            <td id="Td24" style="text-align: left"></td>
                                            <td id="Td25" style="text-align: right"></td>
                                            <td id="Td26" style="text-align: left"></td>
                                        </tr>
                                        <tr id="Tr8" runat="server">
                                            <td id="Td27" style="text-align: right">
                                                <asp:Label ID="Label27" runat="server" Font-Bold="False" Text="Assign Date"></asp:Label></td>
                                            <td id="Td281" style="text-align: left">
                                                <asp:TextBox ID="txtAssignDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"
                                                    Width="130px"></asp:TextBox>
                                                <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                                                    ErrorMessage="Please Select Assign Date" ValidationGroup="assign">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td id="Td29" style="text-align: right">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Text="Due Date"></asp:Label></td>
                                            <td id="Td30" style="text-align: left">
                                                <asp:TextBox ID="txtDueDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"
                                                    Width="130px"></asp:TextBox>
                                                <asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate"
                                                    ErrorMessage="Please Select Due Date">*</asp:RequiredFieldValidator><asp:CompareValidator
                                                        ID="CompareValidator1" runat="server" ControlToCompare="txtAssignDate" ControlToValidate="txtDueDate"
                                                        ErrorMessage="Due Date should not be less than Assign Date" Operator="GreaterThanEqual"
                                                        SetFocusOnError="True" Type="Date" ValidationGroup="assign">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr id="Tr9" runat="server">
                                            <td id="Td31" style="text-align: right"></td>
                                            <td id="Td32" style="text-align: left"></td>
                                            <td id="Td33" style="text-align: right"></td>
                                            <td id="Td34" style="text-align: left"></td>
                                        </tr>
                                        <tr id="Tr10" runat="server">
                                            <td id="Td35" colspan="4" style="text-align: center">
                                                <table id="Table2" align="center">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnAssignTask" runat="server" OnClick="btnAssignTask_Click" Text="Assign"
                                                                ValidationGroup="assign" /></td>
                                                        <td>
                                                            <asp:Button ID="btnCloseAssignTask" runat="server" CausesValidation="False" OnClick="btnCancelTask_Click"
                                                                Text="Close" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Tr11" runat="server">
                                            <td id="Td36" style="text-align: right"></td>
                                            <td id="Td37" style="text-align: left"></td>
                                            <td id="Td38" style="text-align: right"></td>
                                            <td id="Td39" style="text-align: left"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="4" id="TD116" style="height: 417px">
                                      <%--<asp:UpdatePanel ID="upd1" runat="server">
                                            <ContentTemplate>--%>
                                    <table border="0" cellpadding="0" cellspacing="0" id="tblComplaintRegister" runat="server"
                                        visible="False" width="100%">
                                        <tr id="Tr12E" runat="server">
                                            <td class="auto-style5" colspan="4" id="Td40E">General Details</td>
                                        </tr>
                                      
                                        <tr runat="server" id="Tr16">
                                            <td style="text-align: right; height: 19px;" id="Td41"></td>
                                            <td style="text-align: left; height: 19px;" id="Td42"></td>
                                            <td style="text-align: right;" id="Td43" class="auto-style4"></td>
                                            <td style="text-align: left; height: 19px;" id="Td44"></td>
                                        </tr>
                                        <tr runat="server" id="Tr17">
                                            <td style="text-align: right;" id="Td45">
                                                <asp:Label ID="lblQuotationNo" runat="server" Text="CR  No" Width="102px"></asp:Label></td>
                                            <td style="text-align: left" id="Td46">
                                                <asp:TextBox ID="txtCRNo" runat="server" ReadOnly="True"></asp:TextBox><asp:Label ID="lblCRNo" runat ="server" Visible ="false" ></asp:Label></td>
                                            <td style="text-align: right;" id="Td47">
                                                <asp:Label ID="lblCRDate" runat="server" Text="CR Date"></asp:Label></td>
                                            <td style="text-align: left" id="Td48">
                                                <asp:TextBox ID="txtCRDate" runat="server" CssClass="datetext" type="datepic" EnableTheming="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="Tr18">
                                            <td style="text-align: right;" id="Td49">
                                                <asp:Label ID="Label1" runat="server" Text="Call Type" Width="102px"></asp:Label></td>
                                            <td style="text-align: left;" id="Td50">
                                                <asp:DropDownList ID="ddlCallType" runat="server">
                                                    <asp:ListItem>--</asp:ListItem>
                                                    <asp:ListItem>Installation</asp:ListItem>
                                                    <asp:ListItem>Complaint</asp:ListItem>
                                                    <asp:ListItem>Technical Guidance</asp:ListItem>
                                                    <asp:ListItem>AMC</asp:ListItem>
                                                    <asp:ListItem>Non Warranty</asp:ListItem>
                                                    <asp:ListItem>Warranty</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="text-align: right;" id="Td51">Complaint Referred:</td>
                                            <td style="text-align: left; padding-left: 10px;">
                <asp:RadioButtonList ID="rdblIndentfor" CssClass="RadioIndentFor" runat="server" AutoPostBack="True" OnSelectedIndexChanged ="rdblIndentfor_SelectedIndexChanged"
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">Client</asp:ListItem>
                    <asp:ListItem>Executive</asp:ListItem>
                </asp:RadioButtonList></td>
                                        </tr>
                                        <tr id="Tr41" runat="server">
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblSearch" runat="server" Text="Customer Search"></asp:Label></td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtSearchModel" runat="server"></asp:TextBox><asp:Button
                                                    ID="btnSearchModelNo" runat="server" BorderStyle="None" CausesValidation="False"
                                                    CssClass="gobutton" EnableTheming="False" OnClick="btnSearchModelNo_Click" Text="Go" /></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblCompanyName" Visible ="false"  runat="server" Text="Executive Name"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList runat ="server" Visible ="false" ID="ddlSalesExecutive" AutoPostBack="True" OnSelectedIndexChanged ="ddlSalesExecutive_SelectedIndexChanged" ></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtSearchCompany" runat="server"></asp:TextBox><asp:Button
                            ID="btnCompanySearch" runat="server" BorderStyle="None" CausesValidation="False"
                            CssClass="gobutton" EnableTheming="False" OnClick="btnCompanySearch_Click" Text="Go" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <%--<asp:Label  style="text-align: right;" ID="lblCustomer" runat="server" Text="Customer"></asp:Label>--%>
                        Customer :
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                                                </asp:DropDownList><asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                                                    Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomerName"
                                                        ErrorMessage="Please Select the Customer" InitialValue="0">*</asp:RequiredFieldValidator><br />
                                            </td>
                                            <td style="text-align: right;" id="Td57">
                                                <asp:Label ID="lblExecutiveMobile" runat="server" Text="Executive Mobile No."></asp:Label></td>
                                            <td style="text-align: left" id="Td58" class="auto-style1">
                                                <asp:TextBox ID="txtExeMobile" runat ="server" Visible ="false"  ></asp:TextBox>
                                                <%--                        <asp:DropDownList
                            ID="ddlRegion" runat="server" meta:resourcekey="ddlRegionResource1" Visible="true">
                        </asp:DropDownList><asp:Label ID="Label32" runat="server" EnableTheming="False" ForeColor="Red"
                            meta:resourcekey="Label8Resource1" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlRegion" ErrorMessage="Please Select Region "
                                InitialValue="0" Visible="False">*</asp:RequiredFieldValidator>--%></td>
                                        </tr>
                                        <tr runat="server" id="Tr19">
                                            <td style="text-align: right;" id="Td59">
                                                <asp:Label ID="lblCust" runat="server" Text="Company Name :"></asp:Label></td>
                                            <td style="text-align: left" id="Td60">
                                                <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;" id="Td61">
                                                <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                                            <td style="text-align: left" id="Td62">
                                                <asp:DropDownList ID="ddlUnitName" Visible="true" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                    <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                        <tr runat="server" id="Tr20">
                                            <td style="text-align: right;" id="Td63">
                                                <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                                            <td colspan="3" style="text-align: left" id="Td64">
                                                <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="569px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="Tr21">
                                            <td style="text-align: right;" id="Td65">
                                                <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                                            <td style="text-align: left" id="Td66">
                                                <asp:TextBox ID="txtUnitContactPerson" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;" id="Td67">
                                                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                                            <td style="text-align: left" id="Td68">
                                                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="Tr15">
                                            <td id="TD28" style="text-align: right;">
                                                <asp:Label ID="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                                            <td style="text-align: left" id="Td69">
                                                <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td style="text-align: right;" id="Td70">&nbsp;</td>
                                            <td style="text-align: left" id="Td71">&nbsp;</td>
                                        </tr>
                                        <tr id="Tr13" runat="server">
                                            <td style="height: 38px; text-align: right"></td>
                                            <td style="height: 38px; text-align: left"></td>
                                            <td style="text-align: right" class="auto-style3"></td>
                                            <td style="height: 38px; text-align: left"></td>
                                        </tr>
                                             <tr>
                                <td class="profilehead" colspan="4" style="text-align: left; height: 12px;">Complaint Task Details</td>
                            </tr>  
                                        <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:GridView ID="gvCompHistory" runat ="server" AutoGenerateColumns="False"
                                        Width="100%" SelectedRowStyle-BackColor="#c0c0c0" OnRowDataBound ="gvCompHistory_RowDataBound" >
                                        <Columns >
                                            <asp:TemplateField SortExpression="CR No" HeaderText="CR No.">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# Bind("CR_NO") %>' ID="TextBox1"></asp:TextBox>
                                                </EditItemTemplate>

                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>

                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDetCRNo" OnClick="lbtnDetCRNo_Click" ForeColor="#0066ff" runat="server" Text='<%# Eval("CR_NO") %>' CausesValidation="False" __designer:wfdid="w2"></asp:LinkButton>&nbsp; 
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="Start_Dt" HeaderText="CR Date">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Code"></asp:BoundField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NatureofComplaint" HeaderText="Nature of Complaint">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText ="Customer Feed Back" >
                                                            <ItemTemplate >
                                                                <asp:TextBox ID="txtCustFeedback" runat ="server" TextMode ="MultiLine"  Text="<%# Bind('RootCausedNotice') %>"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText ="CorrectiveActionTaken" >
                                                            <ItemTemplate >
                                                                <asp:TextBox ID="txtactionTaken" runat ="server" TextMode ="MultiLine" Text="<%# Bind('CorrectiveActionTaken') %>"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                           <%-- <asp:BoundField DataField="CR_ROOT_CAUSE_NOTICED" HeaderText="Customer Feedback">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CR_CORRECTIVE_ACTION_TAKEN" HeaderText="Corrective Action taken">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>--%>
                                            <%--<asp:TemplateField HeaderText="Attended By" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlAttendedBy" runat="server">
                                                    </asp:DropDownList>
                                                     <asp:HiddenField ID="cthf1" runat="server" Value='<%# Eval("AttendBy") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="AttendBy" HeaderText="Attended By"></asp:BoundField>
                                            <asp:BoundField DataField="Tech_StartDt" HeaderText="Tech Start Dt">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tech_EndDt" HeaderText="Tech End Dt">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TechTimeDuration" HeaderText="Tech Time Duration">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField="CompTimeDuration" HeaderText="Comp Time Duration">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Tech_StartOTP" HeaderText="Start OTP"></asp:BoundField>
                                            <asp:BoundField DataField="Tech_EndOTP" HeaderText="End OTP">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="End_Dt" HeaderText="Closed Dt"></asp:BoundField>
                                            <asp:BoundField DataField="CR_DET_ID" HeaderText="DetId"></asp:BoundField>
                                            <asp:BoundField DataField="CR_ID" HeaderText="CR_ID"></asp:BoundField>
                                             <asp:TemplateField HeaderText ="Remarks" >
                                                            <ItemTemplate >
                                                                <asp:TextBox ID="txtTechRematks" runat ="server" TextMode ="MultiLine"  Text="<%# Bind('Tech_Remarks') %>"  ></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                      <%-- <asp:TemplateField HeaderText="Tech status" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTechStatus" runat="server">
                                                        <asp:ListItem>----</asp:ListItem>
                                                    <asp:ListItem>Open</asp:ListItem>
                                                    <asp:ListItem>Closed</asp:ListItem>
                                                    <asp:ListItem>Pending</asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:HiddenField ID="cthstatus" runat="server" Value='<%# Eval("TECH_Status") %>' />
                                                </ItemTemplate>

                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="TECH_Status" HeaderText="TECH_Status"></asp:BoundField>
                                            <asp:BoundField DataField ="Revised_Key" HeaderText ="Revised key" />
                                            <asp:TemplateField HeaderText ="Time count" >
                                                <ItemTemplate >
                                                    <label class="timeago" title="<%#Eval("Tech_StartDt") %>" style=" font-weight:bold"></label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                         <EmptyDataTemplate>
                                            <span style="font-size: 8pt; color: #ff0033; font-family: Verdana">No Complaint Service</span>

                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                                            </tr> 
                                         <tr>
                                <td>&nbsp;</td>
                            </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center" class="auto-style1">
                                                <asp:Button ID="btncompUpdate" runat ="server" Visible ="false"  Text ="Update" OnClick ="btncompUpdate_Click"  />
                                                <asp:Button ID="btncompDelete" runat ="server" Visible ="false" Enabled ="false"   Text ="Delete" OnClick ="btncompDelete_Click" />
                                                <asp:Button ID="btnCompReopen" runat ="server" Visible ="false" Text ="Reopen" OnClick ="btnCompReopen_Click" />
                                                <asp:Label ID="lblCRDetID" runat="server" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="Tr23">
                                            <td class="profilehead" colspan="4" id="It">Item Details</td>
                                        </tr>
                                        <tr runat="server" id="Tr40">
                                            <td style="text-align: right;"></td>
                                            <td style="text-align: left"></td>
                                            <td style="text-align: right;"></td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        <tr runat="server" id="Tr25">
                                            <td style="text-align: right; height: 40px;" id="Td81">
                                                <asp:Label ID="Label4" runat="server" Text="Model Name :" Width="142px"></asp:Label></td>
                                            <td style="text-align: left; height: 40px;" id="Td82" colspan="3">
                                                <asp:TextBox ID="txtItemName" runat="server" TextMode="MultiLine" Width="681px" EnableTheming="False"></asp:TextBox>&nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="Tr26">
                                            <td style="text-align: right; height: 24px;" id="Td85">
                                                <asp:Label ID="Label10" runat="server" Text="Quantity :" Width="118px"></asp:Label></td>
                                            <td id="Td86" style="height: 24px; text-align: left;" colspan="3">
                                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="Tr29">
                                            <td style="text-align: right;" id="Td94">
                                                <asp:Label ID="Label43" runat="server" Text="Nature of Complaint :" Width="170px"></asp:Label></td>
                                            <td colspan="3" style="text-align: left;" id="Td95">
                                                <asp:TextBox ID="txtNatureofComplaint" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="Tr30">
                                            <td style="text-align: right; height: 48px;" id="Td96">
                                                <asp:Label ID="Label49" runat="server" Text="Customer Feed Back" Width="151px"></asp:Label></td>
                                            <td colspan="3" style="text-align: left; height: 48px;" id="Td97">
                                                <asp:TextBox ID="txtRootCause" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="Tr31">
                                            <td style="text-align: right;" id="Td98">
                                                <asp:Label ID="Label6" runat="server" Text=" Action Taken :" Width="191px"></asp:Label></td>
                                            <td colspan="3" style="text-align: left" id="Td99">
                                                <asp:TextBox ID="txtCorrectiveAction" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                    TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                                        </tr>
                                        <tr runat="server" id="Tr45">
                                            <td style="text-align: right;" id="Td18">
                                                <asp:Label ID="Label9" runat="server" Text=" Attended By :" Width="191px"></asp:Label></td>
                                            <td colspan="3" style="text-align: left" id="Td09">
                                                <asp:DropDownList ID="ddlCrAttendBy"  AutoPostBack ="true"  runat="server">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style ="text-align :right ">
                                                <asp:Label ID="lblTechstatus" runat ="server" Visible ="false" ></asp:Label>
                                            </td>
                                            <td style ="text-align :left ">
                                                <asp:DropDownList ID="ddlTechStatus" runat ="server" Visible ="false" >
                                                    <asp:ListItem>----</asp:ListItem>
                                                    <asp:ListItem>Open</asp:ListItem>
                                                    <asp:ListItem>Closed</asp:ListItem>
                                                    <asp:ListItem>Pending</asp:ListItem>
                                                    <asp:ListItem >ReOpened</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style ="text-align :right "><asp:Label ID="lblTechCRNo" runat ="server" Visible ="false" ></asp:Label></td>
                                            <td style ="text-align :left"><asp:TextBox ID="txtTechCRNo" runat ="server" Visible ="false" ></asp:TextBox></td>
                                        </tr>
                                        <tr id="Tr42" runat="server">
                                            <td colspan="4" style="text-align: center;">&nbsp;<asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                                                CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                                                ValidationGroup="items" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                                    OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                                        </tr>
                                        <tr runat="server" id="Tr32">
                                            <td colspan="4" style="text-align: center">
                                                <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                                                    OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing" Width="100%">
                                                    <Columns>
                                                        <asp:CommandField ShowEditButton="True" />
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                        <asp:BoundField DataField="NatureofComplaint" HeaderText="NatureofComplaint" />
                                                        <asp:BoundField DataField="RootCausedNotice" HeaderText="Customer Feed Back" />
                                                        <asp:BoundField DataField="CorrectiveActionTaken" HeaderText="CorrectiveActionTaken" />
                                                    <asp:BoundField DataField ="AttendBy" HeaderText ="Attend By" />
                                                        <asp:BoundField DataField ="Tech_Status" HeaderText ="Tech Status" />
                                                        <%--<asp:BoundField DataField ="Tech_CRNo" HeaderText ="Tech CRNo" />--%>
                                                        
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">Attachments :</td>
                                            <td>
                                                <asp:FileUpload ID="Uploadattach" runat="server" AllowMultiple="true" />

                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:SqlDataSource ID="sdsUploads" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SELECT * FROM YANTRA_COMPLAINT_REGISTER_ATTACHMENTS WHERE  CR_ID=@CR_IDhidd">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="lblcridHidden" DefaultValue="0" Name="CR_IDhidd" PropertyName="Text" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:Label ID="lblCRIDHidden" runat="server" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Repeater ID="UploadsRepeater" runat="server" DataSourceID="sdsUploads">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnFileOpener" runat="server" CausesValidation="False" OnClick="lbtnFileOpener_Click" Text='<%# Bind("REG_ATTACHMENT") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="Tr33">
                                            <td class="profilehead" colspan="4" style="text-align: left" id="Td102">Reference Details</td>
                                        </tr>
                                        <tr runat="server" id="Tr34">
                                            <td style="text-align: right;" id="Td103"></td>
                                            <td style="text-align: left" id="Td104"></td>
                                            <td style="text-align: right;" id="Td105"></td>
                                            <td style="text-align: left" id="Td106"></td>
                                        </tr>
                                        <tr runat="server" id="Tr35">
                                            <td style="text-align: right; height: 22px;" id="Td107">
                                                <asp:Label ID="Label2" runat="server" Text="Attended By :" Width="88px"></asp:Label></td>
                                            <td style="text-align: left; height: 22px;" id="Td108">
                                                <asp:DropDownList ID="ddlAttendBy" runat="server" AutoPostBack ="true"  OnSelectedIndexChanged ="ddlAttendBy_SelectedIndexChanged">
                                                </asp:DropDownList><asp:TextBox ID="txtTechMobile" runat ="server"   ></asp:TextBox></td>
                                            <td id="Td109" align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Status :"></asp:Label></td>
                                            <td style="text-align: left; height: 22px;" id="Td110">
                                                <asp:DropDownList ID="ddlstatus" runat="server">
                                                    <asp:ListItem>----</asp:ListItem>
                                                    <asp:ListItem>Open</asp:ListItem>
                                                    <asp:ListItem>Closed</asp:ListItem>
                                                    <asp:ListItem>Pending</asp:ListItem>
                                                    <asp:ListItem>ReOpened</asp:ListItem>
                                                    <asp:ListItem >ManuallyClosed</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr runat="server" id="Tr36">
                                            <td id="Td111" align="right">
                                                <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By :"></asp:Label></td>
                                            <td id="Td112">
                                                <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                                                </asp:DropDownList></td>
                                            <td id="Td113" align="right">
                                                <asp:Label ID="Label11" runat="server" Text="Completed  Date"></asp:Label></td>
                                            <td id="Td114">
                                                <asp:TextBox ID="txtCompletedDate" runat="server" type="datepic" CssClass="datetext" EnableTheming="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="Tr37">
                                            <td colspan="4" align="center" id="Td115">
                                                <table id="tblButtons">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                                        <td>
                                                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                                                CausesValidation="False" /></td>
                                                        <td>
                                                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" />
                                                            <asp:Button ID="btnSendSMS" runat="server" Enabled="False" OnClick="btnSendSMS_Click" Text="Send SMS" />
                                                            <asp:Button ID="btnSendMail" runat ="server" Text ="Send Mail" OnClick ="btnSendMail_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"></asp:ValidationSummary>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="items" />
                                              <%--   </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                        </table>
                         <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        
                    </td>
                </tr>
                
                <tr>
                    <td></td>
                    <td>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                            SelectCommand="USP_ServiceCustomer_SEARCH" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtSearchModel" Name="SearchValue" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>
                        <asp:Label ID="lblStartOTP" runat ="server" Visible ="false"  ></asp:Label>
                        <asp:Label ID="lblClosedOTP" runat ="server" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblCRID" runat ="server" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblRootCause" runat ="server" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblAction" runat ="server" Visible ="false" ></asp:Label>
                        <asp:Label ID="lblEndDt" runat ="server" Visible ="false"></asp:Label>
                        <asp:Label ID="lbltechreamrks" runat ="server" Visible ="false"></asp:Label>
                        <asp:Label ID="lblEditStatus" runat ="server" Visible ="false"  ></asp:Label>
                        <asp:Label ID="lblEditExe" runat ="server" Visible ="false"></asp:Label>
                        <asp:Label ID="lblEditTech" runat ="server" Visible ="false"></asp:Label>
                    </td>
                    <td></td>
                </tr>
            </table>
    
        <div id="mask1"></div>
              <asp:Panel ID="pnlmail" runat="server"  BackColor="White"
             Style="z-index:111;background-color: White; position: absolute; left: 35%; top: 175%; border: outset 2px gray;padding:5px;display:none">
         <table>
             <tr>
                  <td style ="color :red " ><h4>Send Mail:</h4></td>
                 
             </tr>
             <tr>
                 <td>Email :</td>
                 <td><asp:TextBox ID="email" runat ="server"  ></asp:TextBox></td>
             </tr>
             <tr><td>Subject :</td>
                 <td><asp:TextBox ID="Subject" runat ="server"  TextMode ="MultiLine"  ></asp:TextBox></td>
             </tr>
               <tr><td>Message</td>
                   <td><asp:TextBox ID="message" runat ="server" TextMode ="MultiLine"  ></asp:TextBox></td>
               </tr>
             <tr style ="text-align :center ">
                 <td colspan ="2">
                     <asp:Button ID="btnsubmit" runat ="server" CssClass="loginbutton" EnableTheming="False"  Text="Send Mail!" OnClick ="btnsubmit_Click" />
                        <asp:Button ID="btnCloseMail" runat ="server" CssClass="loginbutton" EnableTheming="False" Text="Close" />
                    </td>
                          </tr> 
         </table>
     </asp:Panel>
   
       
</asp:Content>



 
