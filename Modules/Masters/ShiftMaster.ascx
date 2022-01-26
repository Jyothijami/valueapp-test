<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShiftMaster.ascx.cs" Inherits="Modules_Masters_ShiftMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="searchhead" colspan="2" style="text-align: left; height: 25px;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        shift master</td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="Label12" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="True" CssClass="textbox"
                                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="SHIFT_ID">Shift ID</asp:ListItem>
                                        <asp:ListItem Value="SHIFT_NAME">Shift Name</asp:ListItem>
                                        <asp:ListItem Value="SHIFT_START_TIME">Start Time</asp:ListItem>
                                        <asp:ListItem Value="SHIFT_END_DATE">End Time</asp:ListItem>
                                        <asp:ListItem Value="SHIFT_BREAK_DUR">Break Duration</asp:ListItem>
                                        <asp:ListItem Value="SHIFT_AVAILABLE_DUR">Available Duration</asp:ListItem>
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
                                        </asp:TextBox><cc1:MaskedEditExtender ID="MaskedEditSearchFromTime" runat="server" DisplayMoney="Left"
                                        Enabled="False" Mask="99:99" MaskType="Time" TargetControlID="txtSearchValueFromDate" AcceptAMPM="True">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="lblCurrentToDate" runat="server" Font-Bold="True" Text="To " Visible="False">
                                        </asp:Label></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:TextBox ID="txtSearchText" runat="server" EnableTheming="True" Width="118px">
                                        </asp:TextBox><cc1:MaskedEditExtender ID="MaskedEditSearchToTime" runat="server" DisplayMoney="Left"
                                        Enabled="False" Mask="99:99" MaskType="Time" TargetControlID="txtSearchText" AcceptAMPM="True">
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
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchTypeHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueFromHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="gvShiftMasterDetails" runat="server" SelectedRowStyle-BackColor="#c0c0c0" AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="gvShiftMasterDetails_RowDataBound"
                AllowSorting="True" DataSourceID="sdsShiftMasterDetails" Width="100%">
                <Columns>
                    <asp:BoundField DataField="SHIFT_NAME" HeaderText="ShiftNameHidden"></asp:BoundField>
                    <asp:BoundField DataField="SHIFT_ID" HeaderText="Shift ID"></asp:BoundField>
                    <asp:TemplateField HeaderText="Shift Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("Shift_type") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnShiftName" ForeColor="#0066ff" OnClick="lbtnShiftName_Click" runat="server" Text="<%# Bind('Shift_NAME') %>"
                                CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="SHIFT_START_TIME" HeaderText="Start Time" DataFormatString="{0:t}" HtmlEncode="False">
                    </asp:BoundField>
                    <asp:BoundField DataField="SHIFT_END_TIME" DataFormatString="{0:t}" HeaderText="End Time"
                        HtmlEncode="False" />
                    <asp:BoundField DataField="SHIFT_BREAK_DUR" HeaderText="Break Duration">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SHIFT_AVAILABLE_DUR" HeaderText="Available Duration">
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsShiftMasterDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_SHIFT_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchTypeHidden" DefaultValue="0" Name="SearchType"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueFromHidden" DefaultValue="0" Name="SearchValueFrom"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 19px; text-align: left">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table id="Table1" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            CausesValidation="False" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblShiftDetails"
                runat="server" visible="false" align="center">
                <tr>
                    <td id="tblDetails" runat="server" colspan="2" style="text-align: right">
                        <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="Table2"
                            runat="server" visible="true">
                            <tr>
                                <td colspan="4" style="text-align: left; height: 20px;" class="profilehead">
                                    General Details</td>
                            </tr>
                            <tr>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                                <td style="height: 19px; text-align: right">
                                </td>
                                <td style="height: 19px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td id="TD22" style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="shift code"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtShiftCode" runat="server" ReadOnly="True"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label2" runat="server" Text="shift name"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtShiftName" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShiftName"
                                        ErrorMessage="Please Enter Shift Master Name">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="Label3" runat="server" Text="start time"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox><cc1:MaskedEditExtender ID="meeStartTime" runat="server" DisplayMoney="Left"
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtStartTime" AcceptAMPM="True">
                                    </cc1:MaskedEditExtender>
                                </td>
                                <td style="text-align: right; height: 24px;">
                                    <asp:Label ID="Label6" runat="server" Text="end time"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox><cc1:MaskedEditExtender ID="meeEndTime" runat="server" DisplayMoney="Left"
                                        Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtEndTime" AcceptAMPM="True">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="break duration (mins)"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtBreakDuration" runat="server"></asp:TextBox></td>
                                <td style="text-align: right">
                                    <asp:Label ID="Label5" runat="server" Text="available time"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtAvailableTime" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                </td>
                                <td style="height: 21px; text-align: left">
                                </td>
                                <td style="height: 21px; text-align: left">
                                </td>
                                <td style="height: 21px; text-align: left">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table id="tblButtons">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 21px">
        </td>
        <td style="height: 21px;">
            &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </td>
    </tr>
</table>
