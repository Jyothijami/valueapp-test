<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseItemType.ascx.cs" Inherits="Modules_Masters_PItemType" %>
    <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                           Purchase Item Type Master Details</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By" meta:resourcekey="Label14Resource1"></asp:Label></td>
                                    <td rowspan="3" style="height: 25px; width: 123px;">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" meta:resourcekey="ddlSearchByResource1" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                            <asp:ListItem Value="PIT_TYPE_ID" meta:resourcekey="ListItemResource2">Item Type ID</asp:ListItem>
                                            <asp:ListItem Value="PIT_TYPE" meta:resourcekey="ListItemResource3">Item Type Name</asp:ListItem>
                                            <asp:ListItem Value="PIT_DESC" meta:resourcekey="ListItemResource4">Description</asp:ListItem>
                                        </asp:DropDownList></td>
                                   <%-- <td rowspan="3" style="width: 17px; height: 25px;">
                                        <asp:Label id="lblSearchtext" runat="server" CssClass="label" Font-Bold="True" Text="Text" Width="37px" EnableTheming="False" meta:resourcekey="lblSearchtextResource1"></asp:Label></td>--%>
                                    <td rowspan="3" style="width: 150px; height: 25px;">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox" meta:resourcekey="txtSearchTextResource1"></asp:TextBox><asp:Image id="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                            Visible="False" meta:resourcekey="imgCurrentDayTasksToDateResource1"></asp:Image></td>
                                    <td rowspan="3" style="height: 25px">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" meta:resourcekey="btnSearchGoResource1" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView id="gvItemTypeDetails" runat="server" AutoGenerateColumns="False"
                    DataSourceID="sdsItemTypeDetails" AllowPaging="True" OnRowDataBound="gvItemTypeDetails_RowDataBound" AllowSorting="True" meta:resourcekey="gvItemTypeDetailsResource1" Width="100%">
                    <columns>
<asp:BoundField DataField="PIT_TYPE" HeaderText="ItemTypeNameHidden" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
<asp:BoundField DataField="PIT_TYPE_ID" HeaderText="ItemType" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
<asp:TemplateField HeaderText="ItemType  Name" meta:resourcekey="TemplateFieldResource1"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("PIt_type") %>' id="TextBox1" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:LinkButton id="lbtnItemTypeName" onclick="lbtnItemTypeName_Click" runat="server" ForeColor="#0066FF" Text="<%# Bind('PIt_Type') %>" CausesValidation="False" meta:resourcekey="lbtnItemTypeNameResource1"></asp:LinkButton> 
</ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:BoundField DataField="PIT_DESC" HeaderText="Description" meta:resourcekey="BoundFieldResource3" NullDisplayText="-">
    <ItemStyle HorizontalAlign="Left" />
</asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="sdsItemTypeDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                    SelectCommand="SP_MASTER_PITEMTYPE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                            PropertyName="Text" Type="String" />
                        <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                            PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                &nbsp;
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
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                        <td style="width: 37px">
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" meta:resourcekey="btnDeleteResource1" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblItemTypeDetails"  runat="server" visible="false">
                    <tr>
                        <td id="tblItDetails" runat="server" colspan="2" style="height: 19px; text-align: right">
                            <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="true">
                                <tr>
            <td colspan="2" style="text-align: left; height: 20px;" class="profilehead">
                General Details</td>
                                </tr>
                                <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                                </tr>
                                <tr>
            <td style="text-align: right; height: 24px;">
                &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Label id="lblItemTypeName" runat="server" Text="ItemType  Name" Width="153px" meta:resourcekey="lblItemTypeNameResource1"></asp:Label></td>
            <td style="text-align: left; height: 24px;"><asp:TextBox id="txtItemTypeName" runat="server" Width="172px" MaxLength="20" meta:resourcekey="txtItemTypeNameResource1"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemTypeName"
                    ErrorMessage="Please Enter Item Type Name" meta:resourcekey="RequiredFieldValidator1Resource1">*</asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
            <td style="text-align: right">
                <asp:Label id="lblDescription" runat="server" Text="Description" Width="105px" meta:resourcekey="lblDescriptionResource1"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtDescription" runat="server" TextMode="MultiLine" meta:resourcekey="txtDescriptionResource1"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 19px; text-align: right">
                        </td>
                    </tr>
                    <tr>
            <td colspan="2" style="height: 49px; text-align: center;">
                <table id="tblButtons" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" meta:resourcekey="btnRefreshResource1" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" meta:resourcekey="btnCloseResource1" /></td>
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
                &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" meta:resourcekey="ValidationSummary1Resource1" />
            </td>
        </tr>
    </table>
