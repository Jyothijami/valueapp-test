<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UnitMaster.ascx.cs" Inherits="Modules_Masters_UnitMaster" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="searchhead" colspan="2" style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left">
                        Unit Master </td>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="UOM_ID">S.No</asp:ListItem>
                                        <asp:ListItem Value="UOM_SHORT_DESC">Unit Short Description</asp:ListItem>
                                        <asp:ListItem Value="UOM_LONG_DESC">Unit Long Description</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3">
                                    </td>
                                <td rowspan="3">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox">
                                    </asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False"></asp:Image></td>
                                <td rowspan="3">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" OnClick="btnSearchGo_Click" Text="Go" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:GridView ID="gvUnitMasterDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataSourceID="sdsUnitMasterDetails" OnRowDataBound="gvUnitMasterDetails_RowDataBound"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="UOM_SHORT_DESC" HeaderText="UnitShortDescHidden"></asp:BoundField>
                    <asp:BoundField DataField="UOM_ID" HeaderText="S.No">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Unit Shot Description">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("UOM_SHORT_DESC") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnUnitMaster" ForeColor="#0066ff" OnClick="lbtnUnitMaster_Click" runat="server"
                                Text="<%# Bind('UOM_SHORT_DESC') %>" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UOM_LONG_DESC" HeaderText="Unit Long Description" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsUnitMasterDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_UOM_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName"
                        ControlID="lblSearchItemHidden"></asp:ControlParameter>
                    <asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue"
                        ControlID="lblSearchValueHidden"></asp:ControlParameter>
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 21px; text-align: left">
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
        <td colspan="2" style="height: 21px">
            <table border="0" cellpadding="0" cellspacing="0" id="tblUnitMasterDetails" runat="server"
                visible="false" width="100%">
                <tr>
                    <td colspan="2" style="text-align: left" class="profilehead">
                        General Details</td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                    </td>
                    <td style="height: 21px; text-align: left;">
                    </td>
                </tr>
                <tr>
                    <td style="height: 21px; text-align: right">
                        <asp:Label ID="lblUnitShort" runat="server" Text="Unit Short Description" Width="161px"></asp:Label></td>
                    <td style="height: 21px; text-align: left;">
                        <asp:TextBox ID="txtUnitShort" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVUnitMaster" runat="server" ControlToValidate="txtUnitShort"
                            ErrorMessage="Please Enter the Unit Short Description">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="height: 8px; text-align: right">
                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
                    <td style="height: 8px; text-align: left;">
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 19px;">
                    </td>
                    <td style="text-align: left; height: 19px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table id="tblButtons" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                                        CausesValidation="False" /></td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False"></asp:ValidationSummary>
        </td>
    </tr>
    <tr>
        <td style="height: 21px">
        </td>
        <td style="height: 21px;">
        </td>
    </tr>
</table>
