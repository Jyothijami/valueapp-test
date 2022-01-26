<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AMCServiceAssignments.ascx.cs" Inherits="Modules_Services_AMCServiceAssignments" %>
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
                SelectCommand="SELECT * FROM [YANTRA_AMC_ASSIGNMENTS],[YANTRA_AMC_ORDER_MAST],[YANTRA_AMC_OA_MAST],&#13;&#10;[YANTRA_CUSTOMER_MAST],[YANTRA_AMC_QUOTATION_MAST],[YANTRA_EMPLOYEE_MAST]&#13;&#10;WHERE [YANTRA_AMC_ASSIGNMENTS].AMCO_ID=[YANTRA_AMC_ORDER_MAST].AMCO_ID &#13;&#10;AND [YANTRA_AMC_ORDER_MAST].AMCQT_ID =YANTRA_AMC_QUOTATION_MAST.AMCQT_ID&#13;&#10;AND [YANTRA_AMC_QUOTATION_MAST].CUST_ID=[YANTRA_CUSTOMER_MAST].CUST_ID&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].AMCOA_ID=[YANTRA_AMC_OA_MAST].OA_ID &#13;&#10;AND [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_AMC_ASSIGNMENTS].EMP_ID&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].AMCA_SCHEDULE_DATE>=@FROM&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].AMCA_SCHEDULE_DATE<=@TO&#13;&#10;AND [YANTRA_AMC_ASSIGNMENTS].EMP_ID=@EMPID AND [YANTRA_AMC_ASSIGNMENTS].EMP_ID<>0">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="01/01/1900"
                        Name="FROM" PropertyName="Text" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="01/01/1900"
                        Name="TO" PropertyName="Text" />
                    <asp:ControlParameter ControlID="lblEmpIdHidden" DefaultValue="0" Name="EMPID" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tblAssignTasks" runat="server" border="0" cellpadding="0" cellspacing="0"
                visible="False" width="100%">
                <tr>
                    <td id="Td1" runat="server" class="profilehead" colspan="4">
                        AMC Service Assignments Details</td>
                </tr>
                <tr>
                    <td id="Td2" runat="server">
                    </td>
                    <td id="Td3" runat="server">
                    </td>
                    <td id="Td4" runat="server">
                    </td>
                    <td id="Td5" runat="server">
                    </td>
                </tr>
                <tr>
                    <td id="Td6" runat="server" style="text-align: right">
                        <asp:Label ID="Label2" runat="server" Text="AMC OA No."></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryNo" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td runat="server" style="text-align: right">
                        <asp:Label ID="Label5" runat="server" Text="AMC OA Date"></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtEnquiryDate" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td runat="server" style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="Customer Name"></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtCustomerName" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td runat="server" style="text-align: right">
                        <asp:Label ID="Label6" runat="server" Text="Assigned To"></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtAssignedTo" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td runat="server" style="text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="Assigned Date"></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtAssignedDate" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td runat="server" style="text-align: right">
                        <asp:Label ID="Label7" runat="server" Text="Due Date"></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtDueDate" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td runat="server" style="text-align: right">
                        <asp:Label ID="Label8" runat="server" Text="Status"></asp:Label>
                    </td>
                    <td runat="server" style="text-align: left">
                        <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td runat="server" style="text-align: right">
                        &nbsp;</td>
                    <td runat="server" style="text-align: left">
                        &nbsp;</td>
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
                    <td id="Td35" colspan="4" style="text-align: center">
                        <table id="Table2">
                            <tr>
                                <td>
                                    <asp:Button ID="btnAssignTask" runat="server" OnClick="btnFollowUp_Click" Text="Follow Up" /></td>
                                <td>
                                    <asp:Button ID="btnUpdateStatus" runat="server" OnClick="btnUpdateStatus_Click" Text="Update Status" /></td>
                                <td>
                                    <asp:Button ID="btnCloseAssignTask" runat="server" CausesValidation="False" OnClick="btnCancelTask_Click"
                                        Text="Close" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td colspan="4" style="text-align: center">
                        <table id="tblUpdateStatus" runat="server" border="0" cellpadding="0" cellspacing="0" visible="False"
                            width="100%">
                            <tr id="Tr13" runat="server">
                                <td id="Td20" runat="server" class="profilehead" colspan="3">
                                    update status</td>
                            </tr>
                            <tr id="Tr14" runat="server">
                                <td id="Td21" style="text-align: center">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem>New</asp:ListItem>
                                        <asp:ListItem>Open</asp:ListItem>
                                        <asp:ListItem>Closed</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button ID="btnUpdateSetStatus" runat="server" OnClick="btnUpdateSetStatus_Click" Text="Update Status" /></td>
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
                <tr runat="server">
                    <td colspan="4" style="text-align: right">
                        <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                            visible="False" width="100%">
                            <tr id="Tr1" runat="server">
                                <td id="Td7" class="profilehead" colspan="2">
                                    follow up details</td>
                            </tr>
                            <tr id="Tr2" runat="server">
                                <td id="Td8" style="height: 19px; text-align: right">
                                </td>
                                <td id="Td9" style="height: 19px; text-align: left">
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td10" style="text-align: right">
                                    <asp:Label ID="Label24" runat="server" Text="Name" Width="76px"></asp:Label>
                                </td>
                                <td id="Td11" style="text-align: left">
                                    <asp:TextBox ID="txtFollowUpName" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="Tr4" runat="server">
                                <td id="Td12" style="text-align: right">
                                    <asp:Label ID="Label23" runat="server" Text="Follow Up Description" Width="76px"></asp:Label>
                                </td>
                                <td id="Td13" style="text-align: left">
                                    <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                        Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="Tr5" runat="server">
                                <td id="Td14" style="text-align: right">
                                </td>
                                <td id="Td15">
                                </td>
                            </tr>
                            <tr id="Tr6" runat="server">
                                <td id="Td16" colspan="2" style="text-align: center">
                                    <table id="Table1">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnFollowUpSave" runat="server" CausesValidation="False" OnClick="btnFollowUpSave_Click"
                                                    Text="Save" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFollowUpRefresh" runat="server" CausesValidation="False" OnClick="btnFollowUpRefresh_Click"
                                                    Text="Refresh" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFollowUpHistory" runat="server" CausesValidation="False" OnClick="btnFollowUpHistory_Click"
                                                    Text="History" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnFollowUpClose" runat="server" CausesValidation="False" OnClick="btnFollowUpClose_Click"
                                                    Text="Close" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td colspan="2" style="text-align: center">
                                    <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                        visible="False" width="100%">
                                        <tr id="Tr7" runat="server">
                                            <td id="Td17" runat="server" class="profilehead" colspan="3">
                                                follow up history</td>
                                        </tr>
                                        <tr id="Tr8" runat="server">
                                            <td id="Td18" runat="server" style="text-align: center">
                                                <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    DataSourceID="sdsFollowDetails" OnRowDataBound="gvFollowUp_RowDataBound">
                                                    <SelectedRowStyle BackColor="LightSteelBlue" />
                                                    <Columns>
                                                        <asp:BoundField DataField="AMC_ASSIGN_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                                            HtmlEncode="False" SortExpression="AMC_ASSIGN_DATE" />
                                                        <asp:BoundField DataField="EMP_ID" HeaderText="EMP_ID" SortExpression="EMP_ID" />
                                                        <asp:BoundField DataField="AMC_ASSIGN_DESC" HeaderText="Description" SortExpression="AMC_ASSIGN_DESC" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="sdsFollowDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                    SelectCommand="SELECT [EMP_ID], [AMC_ASSIGN_DESC], [AMC_ASSIGN_DATE] FROM [YANTRA_AMC_ASSIGNMENTS_FOLLOWUP] WHERE ([AMC_ASSIGN_TASK_ID] = @AMC_ASSIGN_TASK_ID)">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="lblAssignTaskIdHiddenForFollowUp" DefaultValue="0"
                                                            Name="AMC_ASSIGN_TASK_ID" PropertyName="Text" Type="Int64" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:Label ID="lblAssignTaskIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>
                                                &nbsp;<br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr12" runat="server">
                                <td id="Td19" colspan="2">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="assign" />
        </td>
    </tr>
</table>
