<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InstallAssignments.ascx.cs" Inherits="Modules_Services_InstallAssignments" %>
<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table border="0" cellpadding="0" width="750">
    <tr>
        <td class="searchhead" style="width: 732px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left;">
                        Assigned Installation list</td>
                    <td>
                    </td>
                    <td style="text-align: right;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td rowspan="3" style="height: 16px">
                                    </td>
                                <td rowspan="3" style="height: 16px">
                                    </td>
                                <td rowspan="3" style="height: 16px">
                                    </td>
                                <td rowspan="3" style="height: 16px">
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="False" Text="From"></asp:Label></td>
                                <td rowspan="3" style="height: 16px">
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True"
                                        Width="106px"></asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                                    <cc1:CalendarExtender ID="ceSearchFrom" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgFromDate" TargetControlID="txtSearchValueFromDate">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 16px">
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="False" Text="To "></asp:Label></td>
                                <td rowspan="3" style="height: 16px">
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                                    <cc1:CalendarExtender ID="ceSearchValueToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        PopupButtonID="imgToDate" TargetControlID="txtSearchText">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                        Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                        UserDateFormat="MonthDayYear">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 16px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblCPID" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblUserType" runat="server" meta:resourcekey="lblSearchValueHiddenResource1"
                            Visible="False"></asp:Label>
                        <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" style="width: 732px">
            <asp:GridView ID="gvInstallAssignDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" Width="100%" DataSourceID="sdsServiceDetails1" OnRowDataBound="gvInstallAssignDetails_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="IA_ID" HeaderText="IA_IDHidden" SortExpression="IA_ID" />
                    <asp:TemplateField HeaderText="Assign No." SortExpression="IA_NO">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("IA_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lbtnAssignNo" runat="server" OnClick="lbtnAssignNo_Click"
                                Text='<%# Eval("IA_NO") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IA_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                        HtmlEncode="False" SortExpression="IA_DATE" />
                    <asp:TemplateField HeaderText="Customer" SortExpression="CUST_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CUST_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lbtnCustName" runat="server" OnClick="lbtnCustName_Click"
                                Text='<%# Eval("CUST_NAME") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="IA_SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderText="Sch. Date" HtmlEncode="False" SortExpression="IA_SCHEDULE_DATE" />
                    <asp:BoundField DataField="IA_ASSIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Assign Date"
                        HtmlEncode="False" SortExpression="IA_ASSIGN_DATE" />
                    <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Assigned To" SortExpression="EMP_FIRST_NAME">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IA_STATUS" HeaderText="Status" SortExpression="IA_STATUS">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    No Data Exist!
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsServiceDetails1" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="if(@UserType=1)&#13;&#10;SELECT * FROM [YANTRA_INSTALL_ASSIGNMENTS],[YANTRA_SO_MAST],[YANTRA_CUSTOMER_MAST],&#13;&#10;[YANTRA_QUOT_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_EMPLOYEE_MAST],[YANTRA_ENQ_MAST]&#13;&#10;WHERE [YANTRA_INSTALL_ASSIGNMENTS].SO_ID=[YANTRA_SO_MAST].SO_ID &#13;&#10;AND [YANTRA_SO_MAST].QUOT_ID =YANTRA_QUOT_MAST.QUOT_ID&#13;&#10;AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID&#13;&#10;AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID&#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID &#13;&#10;AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID&#13;&#10;AND [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_INSTALL_ASSIGNMENTS].EMP_ID&#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].IA_SCHEDULE_DATE>=@FROM &#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].IA_SCHEDULE_DATE<=@TO&#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].EMP_ID=@EMPID&#13;&#10;else&#13;&#10;&#13;&#10;SELECT * FROM [YANTRA_INSTALL_ASSIGNMENTS],[YANTRA_SO_MAST],[YANTRA_CUSTOMER_MAST],&#13;&#10;[YANTRA_QUOT_MAST],[YANTRA_DELIVERY_CHALLAN_MAST],[YANTRA_EMPLOYEE_MAST],[YANTRA_ENQ_MAST]&#13;&#10;WHERE [YANTRA_INSTALL_ASSIGNMENTS].SO_ID=[YANTRA_SO_MAST].SO_ID &#13;&#10;AND [YANTRA_SO_MAST].QUOT_ID =YANTRA_QUOT_MAST.QUOT_ID&#13;&#10;AND [YANTRA_QUOT_MAST].ENQ_ID=[YANTRA_ENQ_MAST].ENQ_ID&#13;&#10;AND [YANTRA_ENQ_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID&#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].DC_ID=[YANTRA_DELIVERY_CHALLAN_MAST].DC_ID &#13;&#10;AND [YANTRA_DELIVERY_CHALLAN_MAST].SO_ID=[YANTRA_SO_MAST].SO_ID&#13;&#10;AND [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_INSTALL_ASSIGNMENTS].EMP_ID&#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].IA_SCHEDULE_DATE>=@FROM &#13;&#10;AND [YANTRA_INSTALL_ASSIGNMENTS].IA_SCHEDULE_DATE<=@TO&#13;&#10;">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblUserType" DefaultValue="0" Name="UserType" PropertyName="Text" />
                    <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="01/01/1900"
                        Name="FROM" PropertyName="Text" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="12/31/2020"
                        Name="TO" PropertyName="Text" />
                    <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EMPID" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
           
        </td>
    </tr>
    <tr>
        <td style="width: 732px">
            <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="False" width="100%">
                <tr id="Tr1" runat="server">
                    <td class="profilehead" colspan="4">
                        aMC Assignments</td>
                </tr>
                <tr id="Tr2" runat="server">
                    <td id="Td1">
                    </td>
                    <td id="Td2" style="text-align: left">
                    </td>
                    <td id="Td3" style="text-align: right">
                    </td>
                    <td id="Td4" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr3" runat="server">
                    <td id="Td151" style="text-align: right">
                        <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Text="Assign Task No" Width="105px"></asp:Label></td>
                    <td id="Td6" style="text-align: left">
                        <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td7" style="text-align: right">
                    </td>
                    <td id="Td8" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr4" runat="server">
                    <td id="Td9" style="text-align: right">
                        <asp:Label ID="Label5" runat="server" CssClass="label" Font-Bold="False" Text="SO No."></asp:Label></td>
                    <td id="Td10" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryNoForAssign" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="lblSONO" runat="server" Text="Label" Visible="False"></asp:Label></td>
                    <td id="Td11" style="text-align: right">
                        &nbsp;<asp:Label ID="Label13" runat="server" CssClass="label" Font-Bold="False" Text="SO Date"></asp:Label></td>
                    <td id="Td12" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryDateForAssign" runat="server" ReadOnly="True"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="meeEnquiryDateForAssing" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEnquiryDateForAssign"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr id="Tr5" runat="server">
                    <td id="Td13" style="text-align: right">
                        <asp:Label ID="Label15" runat="server" CssClass="label" Font-Bold="False" Text="Customer Name"></asp:Label></td>
                    <td id="Td14" style="text-align: left">
                        <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td15" style="text-align: right">
                        <asp:Label ID="Label16" runat="server" CssClass="label" Font-Bold="False" Text="Contact E-Mail" Width="105px"></asp:Label></td>
                    <td id="Td16" style="text-align: left">
                        <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr6" runat="server">
                    <td id="Td17" style="text-align: right">
                        <asp:Label ID="Label17" runat="server" CssClass="label" Font-Bold="False" Text="Employee Name"></asp:Label></td>
                    <td id="Td118" style="text-align: left">
                        <asp:DropDownList ID="ddlEmpNameForAssign" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmpNameForAssign_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="Label20" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlEmpNameForAssign"
                            ErrorMessage="Please Select the Employee Name" InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                    <td id="Td19" style="text-align: right">
                        <asp:Label ID="Label18" runat="server" CssClass="label" Font-Bold="False" Text="Employee EMail"></asp:Label></td>
                    <td id="Td20" style="text-align: left">
                        <asp:TextBox ID="txtEmpEmailId" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr7" runat="server">
                    <td id="Td21" style="height: 52px; text-align: right">
                        <asp:Label ID="Label28" runat="server" CssClass="label" Font-Bold="False" Text="Remarks"></asp:Label></td>
                    <td id="Td22" colspan="3" style="height: 52px; text-align: left">
                        <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="83%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                runat="server" ControlToValidate="txtRemarksForAssingn" ErrorMessage="Please Enter Remarks"
                                ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr id="Tr8" runat="server">
                    <td id="Td23" style="height: 19px; text-align: right">
                    </td>
                    <td id="Td24" style="height: 19px; text-align: left">
                    </td>
                    <td id="Td25" style="height: 19px; text-align: right">
                    </td>
                    <td id="Td26" style="height: 19px; text-align: left">
                    </td>
                </tr>
                <tr id="Tr9" runat="server">
                    <td id="Td27" style="text-align: right">
                        <asp:Label ID="Label27" runat="server" CssClass="label" Font-Bold="False" Text="Assign Date" Visible="False"></asp:Label></td>
                    <td id="Td281" style="text-align: left">
                        <asp:TextBox ID="txtAssignDate" runat="server" CssClass="datetext" EnableTheming="False"
                            Width="130px" Visible="False"></asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />
                        <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                            ErrorMessage="Please Select Assign Date" ValidationGroup="assign" Visible="False">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                                ID="ceAssignDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgAssignDate"
                                TargetControlID="txtAssignDate">
                            </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeAssingDate" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAssignDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td id="Td29" style="text-align: right">
                        <asp:Label ID="Label7" runat="server" CssClass="label" Font-Bold="False" Text="Due Date" Visible="False"></asp:Label></td>
                    <td id="Td30" style="text-align: left">
                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="datetext" EnableTheming="False"
                            Width="130px" Visible="False"></asp:TextBox><asp:Image ID="imgDueDate" runat="server" ImageUrl="~/Images/Calendar.png" Visible="False" />
                        <asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate"
                            ErrorMessage="Please Select Due Date" ValidationGroup="assign" Visible="False">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                                    ID="ceDueDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgDueDate"
                                    TargetControlID="txtDueDate">
                                </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeDueDate" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDueDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr id="Tr10" runat="server">
                    <td id="Td31" style="text-align: right">
                    </td>
                    <td id="Td32" style="text-align: left">
                    </td>
                    <td id="Td33" style="text-align: right">
                    </td>
                    <td id="Td34" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr11" runat="server">
                    <td id="Td35" colspan="4" style="height: 49px; text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="assign" />
                        <table id="Table2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAssignTask" runat="server" OnClick="btnAssignTask_Click" Text="Assign"
                                        ValidationGroup="assign" Visible="False" /></td>
                                <td style="width: 52px">
                                    <asp:Button ID="btnCloseAssignTask" runat="server" CausesValidation="False" OnClick="btnCancelTask_Click"
                                        Text="Close" /></td>
                                <td style="width: 52px">
                        <asp:Button ID="btnCr" runat="server" CausesValidation="False" OnClick="btnCr_Click"
                            Text="Complaint Register" Width="119px" CssClass="midtab" /></td>
                            </tr>
                        </table>
                        </td>
                </tr>
                <tr id="Tr12" runat="server">
                    <td id="Td36" style="text-align: right">
                    </td>
                    <td id="Td37" style="text-align: left">
                    </td>
                    <td id="Td38" style="text-align: right">
                    </td>
                    <td id="Td39" style="text-align: left">
                    </td>
                </tr>
            </table>
            <table id="tblComplaintRegister" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="False" width="100%">
                <tr id="Tr13" runat="server">
                    <td id="Td40" class="profilehead" colspan="4">
                        general details</td>
                </tr>
                <tr id="Tr16" runat="server">
                    <td id="Td41" style="text-align: right">
                    </td>
                    <td id="Td42" style="text-align: left">
                    </td>
                    <td id="Td43" style="text-align: right">
                    </td>
                    <td id="Td44" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr17" runat="server">
                    <td id="Td45" style="text-align: right">
                        <asp:Label ID="lblQuotationNo" runat="server" Text="CR  No" Width="102px"></asp:Label></td>
                    <td id="Td46" style="text-align: left">
                        <asp:TextBox ID="txtCRNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td47" style="text-align: right">
                        <asp:Label ID="lblCRDate" runat="server" Text="CR Date"></asp:Label></td>
                    <td id="Td48" style="text-align: left">
                        <asp:TextBox ID="txtCRDate" runat="server" CssClass="datetext" EnableTheming="False"></asp:TextBox>&nbsp;<asp:Image
                            ID="imgCRDate" runat="server" ImageUrl="~/Images/Calendar.png" />&nbsp;
                        <cc1:CalendarExtender ID="ceCRDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            PopupButtonID="imgCRDate" TargetControlID="txtCRDate">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeCRDate" runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                            CultureTimePlaceholder="" Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtCRDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr id="Tr18" runat="server">
                    <td id="Td49" style="text-align: right">
                        <asp:Label ID="Label1" runat="server" Text="Call Type" Width="102px"></asp:Label></td>
                    <td id="Td50" style="text-align: left">
                        <asp:DropDownList ID="ddlCallType" runat="server">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem>AMC</asp:ListItem>
                            <asp:ListItem>Installation</asp:ListItem>
                            <asp:ListItem>Non Warranty</asp:ListItem>
                            <asp:ListItem>Warranty</asp:ListItem>
                        </asp:DropDownList></td>
                    <td id="Td51" style="text-align: right">
                    </td>
                    <td id="Td52" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr14" runat="server">
                    <td id="TD5" style="text-align: right">
                    </td>
                    <td id="Td53" style="text-align: left">
                    </td>
                    <td id="Td54" style="text-align: right">
                    </td>
                    <td id="Td55" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr15" runat="server">
                    <td id="TD18" style="text-align: right">
                        <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></td>
                    <td id="Td56" style="text-align: left">
                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerName_SelectedIndexChanged">
                        </asp:DropDownList><asp:Label ID="Label31" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCustomerName"
                            ErrorMessage="Please Select the Customer" InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                    <td id="Td57" style="text-align: right">
                        <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label></td>
                    <td id="Td58" style="text-align: left">
                        <asp:TextBox ID="txtRegion" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr19" runat="server">
                    <td id="Td59" style="text-align: right">
                        <asp:Label ID="Label9" runat="server" Text="Industry Type"></asp:Label></td>
                    <td id="Td60" style="text-align: left">
                        <asp:TextBox ID="txtIndustryType" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td61" style="text-align: right">
                        <asp:Label ID="lblInitName" runat="server" Text="Unit Name" Width="74px"></asp:Label></td>
                    <td id="Td62" style="text-align: left">
                        <asp:DropDownList ID="ddlUnitName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Customer--</asp:ListItem>
                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvUnitName" runat="server" ControlToValidate="ddlUnitName"
                            ErrorMessage="Please Select the Unit Name" InitialValue="0">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr id="Tr20" runat="server">
                    <td id="Td63" style="text-align: right">
                        <asp:Label ID="lblUnitAddress" runat="server" Text="Unit Address" Width="106px"></asp:Label></td>
                    <td id="Td64" colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtUnitAddress" runat="server" CssClass="multilinetext" EnableTheming="False"
                            Font-Names="Verdana" Font-Size="8pt" TextMode="MultiLine" Width="569px"></asp:TextBox></td>
                </tr>
                <tr id="Tr21" runat="server">
                    <td id="Td65" style="text-align: right">
                        <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person"></asp:Label></td>
                    <td id="Td66" style="text-align: left">
                        <asp:TextBox ID="txtContactPerson" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:DropDownList ID="ddlContactPerson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlContactPerson_SelectedIndexChanged"
                            Visible="False">
                            <asp:ListItem Value="0">--</asp:ListItem>
                            <asp:ListItem Value="0">--Select Unit Name--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ControlToValidate="ddlContactPerson"
                            ErrorMessage="Please Select the Contact Person" InitialValue="0" ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                    <td id="Td67" style="text-align: right">
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
                    <td id="Td68" style="text-align: left">
                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr22" runat="server">
                    <td id="TD28" style="text-align: right">
                        <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="74px"></asp:Label></td>
                    <td id="Td69" style="text-align: left">
                        <asp:TextBox ID="txtPhoneNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td70" style="text-align: right">
                        <asp:Label ID="Label8" runat="server" Text="Mobile" Width="74px"></asp:Label></td>
                    <td id="Td71" style="text-align: left">
                        <asp:TextBox ID="txtMobile" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr23" runat="server">
                    <td id="Td72" style="height: 25px; text-align: right">
                    </td>
                    <td id="Td73" style="height: 25px; text-align: left">
                    </td>
                    <td id="Td74" style="height: 25px; text-align: right">
                    </td>
                    <td id="Td75" style="height: 25px; text-align: left">
                    </td>
                </tr>
                <tr id="Tr24" runat="server">
                    <td id="Td76" class="profilehead" colspan="4">
                        item details</td>
                </tr>
                <tr id="Tr25" runat="server">
                    <td id="Td77" style="height: 19px; text-align: right">
                    </td>
                    <td id="Td78" style="height: 19px; text-align: left">
                    </td>
                    <td id="Td79" style="height: 19px; text-align: right">
                    </td>
                    <td id="Td80" style="height: 19px; text-align: left">
                    </td>
                </tr>
                <tr id="Tr26" runat="server">
                    <td id="Td81" style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="Model No" Width="98px"></asp:Label></td>
                    <td id="Td82" style="text-align: left">
                        <asp:DropDownList ID="ddlItemType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged">
                        </asp:DropDownList><asp:Label ID="Label54" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="ddlItemType" ErrorMessage="Please Select the Item Type" InitialValue="0"
                                ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                    <td id="Td83" style="text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="Model Name :" Width="100px"></asp:Label></td>
                    <td id="Td84" style="text-align: left">
                        <asp:TextBox ID="txtItemName" runat="server">
                            </asp:TextBox></td>
                </tr>
                <tr id="Tr27" runat="server">
                    <td style="text-align: right">
                        <asp:Label ID="Label32" runat="server" Text="Item Category :"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtItemCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    <td style="text-align: right">
                        <asp:Label ID="Label33" runat="server" Text="Item SubCategory :"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtItemSubCategory" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                </tr>
                <tr id="Tr28" runat="server">
                    <td style="text-align: right">
                        <asp:Label ID="Label12" runat="server" Text="Color :"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtColor" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                    <td style="text-align: right">
                        <asp:Label ID="Label55" runat="server" Text="Brand :"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtBrand" runat="server" ReadOnly="True">
                            </asp:TextBox></td>
                </tr>
                <tr id="Tr29" runat="server">
                    <td id="Td85" style="text-align: right">
                        <asp:Label ID="Label2" runat="server" Text="Serial No." Width="63px"></asp:Label></td>
                    <td id="Td86">
                        <asp:TextBox ID="txtSerialNo" runat="server"></asp:TextBox></td>
                    <td id="Td87" style="text-align: right">
                        <asp:Label ID="Label10" runat="server" Text="Quantity" Width="63px"></asp:Label></td>
                    <td id="Td88" style="text-align: left">
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox><asp:Label ID="Label11"
                            runat="server" EnableTheming="False" ForeColor="Red" Text="*"></asp:Label><asp:RequiredFieldValidator
                                ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Please Enter the Quantity"
                                ValidationGroup="items">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr id="Tr30" runat="server">
                    <td id="Td94" style="height: 48px; text-align: right">
                        <asp:Label ID="Label43" runat="server" Text="Nature of Complaint"></asp:Label></td>
                    <td id="Td95" colspan="3" style="height: 48px; text-align: left">
                        <asp:TextBox ID="txtNatureofComplaint" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                </tr>
                <tr id="Tr31" runat="server">
                    <td id="Td96" style="text-align: right">
                        <asp:Label ID="Label49" runat="server" Text="Root Cause Noticed"></asp:Label></td>
                    <td id="Td97" colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtRootCause" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                </tr>
                <tr id="Tr32" runat="server">
                    <td id="Td98" style="text-align: right">
                        <asp:Label ID="Label6" runat="server" Text="Corrective Action Taken"></asp:Label></td>
                    <td id="Td99" colspan="3" style="text-align: left">
                        <asp:TextBox ID="txtCorrectiveAction" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="94%">-</asp:TextBox></td>
                </tr>
                <tr id="Tr42" runat="server">
                    <td colspan="4" style="text-align: center">
                        &nbsp;<asp:Button ID="btnAdd" runat="server" BackColor="Transparent" BorderStyle="None"
                            CssClass="loginbutton" EnableTheming="False" OnClick="btnAdd_Click" Text="Add"
                            ValidationGroup="items" /><asp:Button ID="btnItemRefresh" runat="server" BackColor="Transparent"
                                BorderStyle="None" CausesValidation="False" CssClass="loginbutton" EnableTheming="False"
                                OnClick="btnItemRefresh_Click" Text="Refresh" /></td>
                </tr>
                <tr id="Tr33" runat="server">
                    <td colspan="4" style="text-align: center">
                        <asp:GridView ID="gvQuotationItems" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotationItems_RowDataBound"
                            OnRowDeleting="gvQuotationItems_RowDeleting" OnRowEditing="gvQuotationItems_RowEditing">
                            <Columns>
                                <asp:CommandField ShowEditButton="True" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:BoundField DataField="ItemCode" HeaderText="Item Code">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemType" HeaderText="Item Type">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ItemName" HeaderText="Item Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SerialNo" HeaderText="Serial No." NullDisplayText="-" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeIdHidden" />
                                <asp:BoundField DataField="NatureofComplaint" HeaderText="NatureofComplaint" />
                                <asp:BoundField DataField="RootCausedNotice" HeaderText="RootCausedNotice" />
                                <asp:BoundField DataField="CorrectiveActionTaken" HeaderText="CorrectiveActionTaken" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="Tr34" runat="server">
                    <td id="Td102" class="profilehead" colspan="4" style="text-align: left">
                        Reference Details</td>
                </tr>
                <tr id="Tr35" runat="server">
                    <td id="Td103" style="text-align: right">
                    </td>
                    <td id="Td104" style="text-align: left">
                    </td>
                    <td id="Td105" style="text-align: right">
                    </td>
                    <td id="Td106" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr36" runat="server">
                    <td id="Td107" style="text-align: right">
                        <asp:Label ID="lblPreparedBy" runat="server" Text="Prepared By"></asp:Label></td>
                    <td id="Td108" style="text-align: left">
                        <asp:DropDownList ID="ddlPreparedBy" runat="server" Enabled="False">
                        </asp:DropDownList></td>
                    <td id="Td109" style="text-align: right">
                    </td>
                    <td id="Td110" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr37" runat="server">
                    <td id="Td111">
                    </td>
                    <td id="Td112">
                    </td>
                    <td id="Td113">
                    </td>
                    <td id="Td114">
                    </td>
                </tr>
                <tr id="Tr38" runat="server">
                    <td id="Td115" align="center" colspan="4">
                        <table id="tblButtons">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save & Assign" ValidationGroup="assign" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" OnClick="btnRefresh_Click"
                                        Text="Refresh" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" CausesValidation="False" OnClick="btnClose_Click"
                                        Text="Close" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
