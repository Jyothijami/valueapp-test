<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AMCAssignments.ascx.cs" Inherits="Modules_Services_SAMCAssignments" %>
<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table border="0" cellpadding="0" width="750">
    
    <tr>
        <td class="searchhead">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Assigned AMC list</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    </td>
                                <td rowspan="3" style="height: 25px">
                                    </td>
                                <td rowspan="3" style="height: 25px">
                                    </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="False" Text="From"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True"
                                        Width="106px"></asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                                    <cc1:calendarextender id="ceSearchFrom" runat="server" enabled="True" format="dd/MM/yyyy"
                                        popupbuttonid="imgFromDate" targetcontrolid="txtSearchValueFromDate"></cc1:calendarextender>
                                    <cc1:maskededitextender id="MaskedEditSearchFromDate" runat="server" displaymoney="Left"
                                        enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchValueFromDate"
                                        userdateformat="MonthDayYear"></cc1:maskededitextender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="False" Text="To "></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px"></asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                                    <cc1:calendarextender id="ceSearchValueToDate" runat="server" enabled="True" format="dd/MM/yyyy"
                                        popupbuttonid="imgToDate" targetcontrolid="txtSearchText"></cc1:calendarextender>
                                    <cc1:maskededitextender id="MaskedEditSearchToDate" runat="server" displaymoney="Left"
                                        enabled="True" mask="99/99/9999" masktype="Date" targetcontrolid="txtSearchText"
                                        userdateformat="MonthDayYear"></cc1:maskededitextender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label
                            ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:GridView ID="gvAMCAssignDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                DataSourceID="sdsServiceDetails"
                Width="100%" OnRowDataBound="gvAMCAssignDetails_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="AMCA_ID" HeaderText="AMCA_IDHidden" SortExpression="AMCA_ID" />
                    <asp:TemplateField HeaderText="Assign No." SortExpression="AMCA_NO">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("AMCA_NO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lbtnAssignNo" runat="server" OnClick="lbtnAssignNo_Click"
                                Text='<%# Eval("AMCA_NO") %>' CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AMCA_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                        HtmlEncode="False" SortExpression="AMCA_DATE" />
                    <asp:TemplateField HeaderText="Customer" SortExpression="CUST_NAME">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CUST_NAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            &nbsp;<asp:LinkButton ID="lbtnCustName" runat="server" Text='<%# Eval("CUST_NAME") %>' OnClick="lbtnCustName_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AMCA_SCHEDULE_DATE" DataFormatString="{0:dd/MM/yyyy}"
                        HeaderText="Sch. Date" HtmlEncode="False" SortExpression="AMCA_SCHEDULE_DATE" />
                    <asp:BoundField DataField="AMCA_ASSIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Assign Date"
                        HtmlEncode="False" SortExpression="AMCA_ASSIGN_DATE" />
                    <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Assigned To" SortExpression="EMP_FIRST_NAME">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AMCA_STATUS" HeaderText="Status" SortExpression="AMCA_STATUS">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataTemplate>
                    No Data Exist!
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsServiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SELECT * FROM [YANTRA_AMC_ASSIGNMENTS],YANTRA_COMPLAINT_REGISTER,[YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_OA_MAST],&#13;&#10;[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_EMPLOYEE_MAST]&#13;&#10;WHERE [YANTRA_AMC_ASSIGNMENTS].AMCO_ID=[YANTRA_AMC_ORDER_MAST].AMCO_ID &#13;&#10;AND [YANTRA_AMC_ORDER_MAST].AMCQT_ID =YANTRA_AMC_QUOTATION_MAST.AMCQT_ID&#13;&#10;AND [YANTRA_AMC_QUOTATION_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].AMCOA_ID=[YANTRA_AMC_OA_MAST].OA_ID &#13;&#10;AND [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_AMC_ASSIGNMENTS].EMP_ID&#13;&#10;AND [YANTRA_COMPLAINT_REGISTER].CR_CALL_TYPE = 'AMC'&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].AMCA_SCHEDULE_DATE>=@FROM&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].AMCA_SCHEDULE_DATE<=@TO">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="01/01/1900"
                        Name="FROM" PropertyName="Text" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="01/01/1900"
                        Name="TO" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="False" width="100%">
                <tr runat="server">
                    <td class="profilehead" colspan="4">
                        aMC Assignments</td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td id="Td1">
                    </td>
                    <td id="Td2" style="text-align: left">
                    </td>
                    <td id="Td3" style="text-align: right">
                    </td>
                    <td id="Td4" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr2" runat="server">
                    <td id="Td151" style="text-align: right">
                        <asp:Label ID="Label19" runat="server" CssClass="label" Font-Bold="False" Text="Assign Task No"></asp:Label></td>
                    <td id="Td6" style="text-align: left">
                        <asp:TextBox ID="txtAssignTaskNo" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td7" style="text-align: right">
                    </td>
                    <td id="Td8" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr3" runat="server">
                    <td id="Td9" style="text-align: right">
                        <asp:Label ID="Label5" runat="server" CssClass="label" Font-Bold="False" Text="AMC PO No."></asp:Label></td>
                    <td id="Td10" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryNoForAssign" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td11" style="text-align: right">
                        &nbsp;<asp:Label ID="Label13" runat="server" CssClass="label" Font-Bold="False" Text="AMC OP Date"></asp:Label></td>
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
                <tr id="Tr4" runat="server">
                    <td id="Td13" style="text-align: right">
                        <asp:Label ID="Label15" runat="server" CssClass="label" Font-Bold="False" Text="Customer Name"></asp:Label></td>
                    <td id="Td14" style="text-align: left">
                        <asp:TextBox ID="txtCustomerNameForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                    <td id="Td15" style="text-align: right">
                        <asp:Label ID="Label16" runat="server" CssClass="label" Font-Bold="False" Text="Contact E-Mail"></asp:Label></td>
                    <td id="Td16" style="text-align: left">
                        <asp:TextBox ID="txtCustomerEmailForAssingn" runat="server" ReadOnly="True"></asp:TextBox></td>
                </tr>
                <tr id="Tr5" runat="server">
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
                <tr id="Tr6" runat="server">
                    <td id="Td21" style="height: 52px; text-align: right">
                        <asp:Label ID="Label28" runat="server" CssClass="label" Font-Bold="False" Text="Remarks"></asp:Label></td>
                    <td id="Td22" colspan="3" style="height: 52px; text-align: left">
                        <asp:TextBox ID="txtRemarksForAssingn" runat="server" CssClass="multilinetext" EnableTheming="False"
                            TextMode="MultiLine" Width="83%"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                runat="server" ControlToValidate="txtRemarksForAssingn" ErrorMessage="Please Enter Remarks"
                                ValidationGroup="assign">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr id="Tr7" runat="server">
                    <td id="Td23" style="text-align: right; height: 19px;">
                    </td>
                    <td id="Td24" style="text-align: left; height: 19px;">
                    </td>
                    <td id="Td25" style="text-align: right; height: 19px;">
                    </td>
                    <td id="Td26" style="text-align: left; height: 19px;">
                    </td>
                </tr>
                <tr id="Tr8" runat="server">
                    <td id="Td27" style="text-align: right">
                        <asp:Label ID="Label27" runat="server" CssClass="label" Font-Bold="False" Text="Assign Date"></asp:Label></td>
                    <td id="Td281" style="text-align: left">
                        <asp:TextBox ID="txtAssignDate" runat="server" CssClass="datetext" EnableTheming="False"
                            Width="130px"></asp:TextBox><asp:Image ID="imgAssignDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                        <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAssignDate"
                            ErrorMessage="Please Select Assign Date" ValidationGroup="assign">*</asp:RequiredFieldValidator><cc1:CalendarExtender
                                ID="ceAssignDate" runat="server" Enabled="True" PopupButtonID="imgAssignDate"
                                TargetControlID="txtAssignDate" Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeAssingDate" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtAssignDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                    <td id="Td29" style="text-align: right">
                        <asp:Label ID="Label7" runat="server" CssClass="label" Font-Bold="False" Text="Due Date"></asp:Label></td>
                    <td id="Td30" style="text-align: left">
                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="datetext" EnableTheming="False"
                            Width="130px"></asp:TextBox><asp:Image ID="imgDueDate" runat="server" ImageUrl="~/Images/Calendar.png" />
                        <asp:Label ID="Label22" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDueDate"
                            ErrorMessage="Please Select Due Date" ValidationGroup="assign">*</asp:RequiredFieldValidator><asp:CompareValidator
                                ID="CompareValidator1" runat="server" ControlToCompare="txtAssignDate" ControlToValidate="txtDueDate"
                                ErrorMessage="Due Date should not be less than Assign Date" Operator="GreaterThanEqual"
                                SetFocusOnError="True" Type="Date" ValidationGroup="assign">*</asp:CompareValidator><cc1:CalendarExtender
                                    ID="ceDueDate" runat="server" Enabled="True" PopupButtonID="imgDueDate" TargetControlID="txtDueDate" Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="meeDueDate" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDueDate"
                            UserDateFormat="MonthDayYear">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr id="Tr9" runat="server">
                    <td id="Td31" style="text-align: right">
                    </td>
                    <td id="Td32" style="text-align: left">
                    </td>
                    <td id="Td33" style="text-align: right">
                    </td>
                    <td id="Td34" style="text-align: left">
                    </td>
                </tr>
                <tr id="Tr10" runat="server">
                    <td id="Td35" colspan="4" style="height: 49px; text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="assign" />
                        <table id="Table2">
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
        </td>
    </tr>
</table>
