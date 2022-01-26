<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AMCAssignments.aspx.cs" Inherits="Modules_Services_AMCAssignments" Title="|| YANTRA : Services : AMC Assignments ||" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" class="pagehead">
        <tr>
            <td>
                AMC assignments</td>
        </tr>
    </table>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="searchhead">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="text-align: left">
                                                    Assigned
                                        enquiries list</td>
                                                <td>
                                                </td>
                                                <td style="text-align: right">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:Label ID="Label1" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                                                    Text="Search By"></asp:Label></td>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                    OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--</asp:ListItem>
                                                                    <asp:ListItem Value="AMC_ASSIGN_TASK_NO">AssignTask No.</asp:ListItem>
                                                                    <asp:ListItem Value="ENQ_DATE">Enquiry Date</asp:ListItem>
                                                                    <asp:ListItem Value="CUST_NAME">Customer Name</asp:ListItem>
                                                                    <asp:ListItem Value="ENQ_ORIG_BY">Orginated By</asp:ListItem>
                                                                    <asp:ListItem Value="ENQ_DELIVERY_DATE">Delivery Date</asp:ListItem>
                                                                    <asp:ListItem Value="AMC_ASSIGN_DATE">Assign Date</asp:ListItem>
                                                                    <asp:ListItem Value="AMC_DUE_DATE">Due Date</asp:ListItem>
                                                                    <asp:ListItem Value="EMP_FIRST_NAME">Assigned To</asp:ListItem>
                                                                    <asp:ListItem Value="AMC_ASSIGN_STATUS">Status</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:DropDownList ID="ddlSymbols" runat="server" AutoPostBack="True" CssClass="textbox"
                                                                    EnableTheming="False" OnSelectedIndexChanged="ddlSymbols_SelectedIndexChanged"
                                                                    Visible="False" Width="50px">
                                                                    <asp:ListItem Selected="True">=</asp:ListItem>
                                                                    <asp:ListItem>&lt;</asp:ListItem>
                                                                    <asp:ListItem>&gt;</asp:ListItem>
                                                                    <asp:ListItem>&lt;=</asp:ListItem>
                                                                    <asp:ListItem>&gt;=</asp:ListItem>
                                                                    <asp:ListItem>R</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:Label ID="lblCurrentFromDate" runat="server" Font-Bold="True" Text="From" Visible="False">
                                        </asp:Label></td>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:TextBox ID="txtSearchValueFromDate" runat="server" EnableTheming="True" Visible="False"
                                                                    Width="106px">
                                        </asp:TextBox><asp:Image ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                                    Visible="False" />
                                                                <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchFrom" runat="server" Enabled="False" PopupButtonID="imgFromDate"
                                                                    TargetControlID="txtSearchValueFromDate">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditSearchFromDate" runat="server" DisplayMoney="Left"
                                                                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchValueFromDate"
                                                                    UserDateFormat="MonthDayYear">
                                                                </cc1:MaskedEditExtender>
                                                            </td>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                                            <td rowspan="3" style="height: 25px">
                                                                <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><asp:Image ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                                                    Visible="False" />
                                                                <cc1:CalendarExtender  Format="MM/dd/yyyy" ID="ceSearchValueToDate" runat="server" Enabled="False" PopupButtonID="imgToDate"
                                                                    TargetControlID="txtSearchText">
                                                                </cc1:CalendarExtender>
                                                                <cc1:MaskedEditExtender ID="MaskedEditSearchToDate" runat="server" DisplayMoney="Left"
                                                                    Enabled="False" Mask="99/99/9999" MaskType="Date" TargetControlID="txtSearchText"
                                                                    UserDateFormat="MonthDayYear">
                                                                </cc1:MaskedEditExtender>
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
                                                    <asp:Label ID="lblEmpIdHidden" runat="server" Visible="False"></asp:Label><asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvAssignDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="sdsEnqAssignDetails" OnRowDataBound="gvAssignDetails_RowDataBound"
                                            Width="100%">
                                            <Columns>
<asp:BoundField DataField="AMC_ASSIGN_TASK_ID" SortExpression="AMC_ASSIGN_TASK_ID" HeaderText="AssignTaskIdHidden"></asp:BoundField>
<asp:BoundField ReadOnly="True" DataField="ENQ_ID" SortExpression="ENQ_ID" HeaderText="EnqIdHidden"></asp:BoundField>
<asp:TemplateField SortExpression="ENQ_NO" HeaderText="Assign Task No"><EditItemTemplate>
                                                        &nbsp;
                                                    
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
                                                        <asp:LinkButton ID="lbtnAssignTask" runat="server" CausesValidation="False" OnClick="lbtnAssignTaskNo_Click"
                                                            Text='<%# Eval("ASSIGN_TASK_NO") %>'></asp:LinkButton>
                                                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ENQ_NO" SortExpression="ENQ_NO" HeaderText="Enquiry No"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ENQ_DATE" SortExpression="ENQ_DATE" HeaderText="Enq Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="CUST_NAME" SortExpression="CUST_NAME" HeaderText="Customer Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="ENQ_ORIG_BY" SortExpression="ENQ_ORIG_BY" HeaderText="Orginated By">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="ENQ_DELIVERY_DATE" SortExpression="ENQ_DELIVERY_DATE" HeaderText="Delivery Date">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="AMC_ASSIGN_DATE" SortExpression="AMC_ASSIGN_DATE" HeaderText="Assign Date"></asp:BoundField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:MM/dd/yyyy}" DataField="AMC_DUE_DATE" SortExpression="AMC_DUE_DATE" HeaderText="Due Date"></asp:BoundField>
<asp:BoundField DataField="EMP_FIRST_NAME" SortExpression="EMP_FIRST_NAME" HeaderText="Assigned To">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="AMC_ASSIGN_STATUS" SortExpression="AMC_ASSIGN_STATUS" HeaderText="Status">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</Columns>
                                            <emptydatatemplate>
No Records Found!
</emptydatatemplate>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="sdsEnqAssignDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                            SelectCommand="SP_SERVICES_AMCASSIGNMENTS_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchType" ControlID="lblSearchTypeHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValueFrom" ControlID="lblSearchValueFromHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="EMPID" ControlID="lblEmpIdHidden"></asp:ControlParameter>
</SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 19px">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="Table1">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnSendQuotation" runat="server" CausesValidation="False" Text="Set Quotation" OnClick="btnSendQuotation_Click" /></td>
                                                <td>
                                                    <asp:Button ID="btnFollowUp" runat="server" CausesValidation="False" Text="Follow Up" OnClick="btnFollowUp_Click" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tblFollowUp" runat="server" border="0" cellpadding="0" cellspacing="0"
                                            visible="false" width="100%">
                                            <tr>
                                                <td class="profilehead" colspan="2">
                                                    follow up details</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 19px; text-align: right">
                                                </td>
                                                <td style="height: 19px; text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label24" runat="server" Text="Name" Width="76px"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtFollowUpName" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label23" runat="server" Text="Follow Up Description" Width="76px"></asp:Label></td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtFollowUpDesc" runat="server" CssClass="multilinetext" EnableTheming="False"
                                                        Height="40px" TextMode="MultiLine" Width="560px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table id="Table2">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnFollowUpSave" runat="server" CausesValidation="False" OnClick="btnFollowUpSave_Click"
                                                                    Text="Save" /></td>
                                                            <td>
                                                                <asp:Button ID="btnFollowUpRefresh" runat="server" CausesValidation="False" OnClick="btnFollowUpRefresh_Click"
                                                                    Text="Refresh" /></td>
                                                            <td>
                                                                <asp:Button ID="btnFollowUpHistory" runat="server" CausesValidation="False" OnClick="btnFollowUpHistory_Click"
                                                                    Text="History" /></td>
                                                            <td>
                                                                <asp:Button ID="btnFollowUpClose" runat="server" CausesValidation="False" OnClick="btnFollowUpClose_Click"
                                                                    Text="Close" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <table id="tblFollowUpHistory" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                        width="100%" visible="false">
                                                        <tr>
                                                            <td class="profilehead" colspan="3">
                                                                follow up history</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvFollowUp" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                    DataSourceID="sdsFollowUp" >
                                                                    <Columns>
                                                                        <asp:BoundField DataField="FU_DATE" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date"
                                                                            HtmlEncode="False" SortExpression="FU_DATE" />
                                                                        <asp:BoundField DataField="EMP_FIRST_NAME" HeaderText="Name" SortExpression="EMP_FIRST_NAME" >
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="FU_DESC" HeaderText="Description" SortExpression="FU_DESC" >
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <SelectedRowStyle BackColor="LightSteelBlue" />
                                                                </asp:GridView>
                                                                <asp:Label ID="lblAssignTaskIdHiddenForFollowUp" runat="server" Visible="False"></asp:Label>
                                                                <asp:SqlDataSource ID="sdsFollowUp" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                                                                    SelectCommand="SELECT   [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].[FU_DATE],[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].[FU_DESC],[YANTRA_EMPLOYEE_MAST].[EMP_FIRST_NAME] FROM [YANTRA_EMPLOYEE_MAST],[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET]&#13;&#10;WHERE [YANTRA_EMPLOYEE_MAST].EMP_ID=[YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].EMP_ID AND [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].ASSIGN_TASK_ID=@ASSIGNID&#13;&#10; ORDER BY [YANTRA_ENQ_ASSIGN_FOLLOWUP_DET].[FU_DATE] DESC">
                                                                    <SelectParameters>
                                                                        <asp:ControlParameter ControlID="lblAssignTaskIdHiddenForFollowUp" DefaultValue="0"
                                                                            Name="ASSIGNID" PropertyName="Text" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; height: 19px;">
                                                </td>
                                                <td style="height: 19px">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
</asp:Content>


 
