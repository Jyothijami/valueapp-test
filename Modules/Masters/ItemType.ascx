<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemType.ascx.cs" Inherits="Modules_Masters_ItemType" %>
<table style="width: 100%">
        <tr>
            <td colspan="2 " style="height: 30px; text-align: left; font-size: 16px"></td>
            <td style="text-align: right">
                <asp:DropDownList ID="ddlNoOfRecords" runat="server" OnSelectedIndexChanged="ddlNoOfRecords_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
<br />
<table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td class="searchhead" colspan="2" style="text-align: left;">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left;">Item Sub Category Details</td>
                    <td></td>
                    <td style="text-align: right;">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By" meta:resourcekey="Label14Resource1"></asp:Label></td>
                                <td rowspan="3" style="height: 25px; width: 123px;">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" meta:resourcekey="ddlSearchByResource1" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">--</asp:ListItem>
                                        <asp:ListItem Value="IT_TYPE_ID" meta:resourcekey="ListItemResource2">S.No</asp:ListItem>
                                        <asp:ListItem Value="IT_TYPE" meta:resourcekey="ListItemResource3">Sub Category Name</asp:ListItem>
                                        <asp:ListItem Value="ITEM_CATEGORY_NAME">Category Name</asp:ListItem>
                                        <asp:ListItem Value="IT_DESC" meta:resourcekey="ListItemResource4">Description</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3" style="width: 17px; height: 25px;">
                                    <asp:Label ID="lblSearchtext" runat="server" CssClass="label" Font-Bold="True" Text="Text" Width="37px" EnableTheming="False" meta:resourcekey="lblSearchtextResource1"></asp:Label></td>
                                <td rowspan="3" style="width: 150px; height: 25px;">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox" Width="111px" meta:resourcekey="txtSearchTextResource1"></asp:TextBox><asp:Image ID="imgCurrentDayTasksToDate" runat="server" ImageUrl="~/Images/Calendar.png"
                                        Visible="False" meta:resourcekey="imgCurrentDayTasksToDateResource1"></asp:Image></td>
                                <td rowspan="3" style="height: 25px">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" meta:resourcekey="btnSearchGoResource1" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False" meta:resourcekey="lblSearchItemHiddenResource1"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False" meta:resourcekey="lblSearchValueHiddenResource1"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="gvItemTypeDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False"
                DataSourceID="sdsItemTypeDetails" AllowPaging="True" OnRowDataBound="gvItemTypeDetails_RowDataBound" AllowSorting="True" meta:resourcekey="gvItemTypeDetailsResource1" Width="100%">
                <Columns>
                    <asp:BoundField DataField="IT_TYPE" HeaderText="SubCatergoryNameHidden" meta:resourcekey="BoundFieldResource1"></asp:BoundField>
                    <asp:BoundField DataField="IT_TYPE_ID" HeaderText="S.No" meta:resourcekey="BoundFieldResource2"></asp:BoundField>
                    <asp:TemplateField HeaderText="Sub Category Name" meta:resourcekey="TemplateFieldResource1">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("It_type") %>' ID="TextBox1" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnItemTypeName" ForeColor="#0066ff" OnClick="lbtnItemTypeName_Click" runat="server" Text="<%# Bind('It_Type') %>" CausesValidation="False" meta:resourcekey="lbtnItemTypeNameResource1"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Category Name">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="IT_DESC" HeaderText="Description" meta:resourcekey="BoundFieldResource3" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ITEM_CATEGORY_ID" HeaderText="Category Id" />
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsItemTypeDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>"
                SelectCommand="SP_MASTER_ITEMTYPE_SEARCH_SELECT" SelectCommandType="StoredProcedure">
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
        <td colspan="2" style="height: 19px; text-align: left"></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table id="Table1" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" meta:resourcekey="btnNewResource1" /></td>
                    <td style="width: 37px">
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" meta:resourcekey="btnEditResource1" /></td>
                    <td style="width: 58px">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" meta:resourcekey="btnDeleteResource1" /></td>
                    <td style="width: 40px">
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Print " /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblItemTypeDetails" runat="server" visible="false" align="center">
                <tr>
                    <td id="tblItDetails" runat="server" colspan="2" style="height: 19px; text-align: right">
                        <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="Table2" runat="server" visible="true">
                            <tr>
                                <td colspan="2" style="text-align: left; height: 20px;" class="profilehead">General Details</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 20px; text-align: left"></td>
                            </tr>
                            <tr>
                                <td style="height: 21px; text-align: right">
                                    <asp:Label ID="lblCategoryName" runat="server" meta:resourcekey="lblItemTypeNameResource1"
                                        Text="Category Name" Width="153px"></asp:Label></td>
                                <td style="height: 21px; text-align: left">
                                    <asp:DropDownList ID="ddlCategoryName" runat="server" Width="151px" CausesValidation="True">
                                    </asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategoryName"
                                        ErrorMessage="Please Select Category Name" meta:resourcekey="RequiredFieldValidator1Resource1" InitialValue="0">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right; height: 24px;">&nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Label ID="lblItemTypeName" runat="server" Text="Sub Category Name" Width="153px" meta:resourcekey="lblItemTypeNameResource1"></asp:Label></td>
                                <td style="text-align: left; height: 24px;">
                                    <asp:TextBox ID="txtItemTypeName" runat="server" Width="172px" meta:resourcekey="txtItemTypeNameResource1"></asp:TextBox>
                                    <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                                        Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemTypeName"
                                        ErrorMessage="Please Enter Sub Category Name" meta:resourcekey="RequiredFieldValidator1Resource1">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblDescription" runat="server" Text="Description" Width="105px" meta:resourcekey="lblDescriptionResource1"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" meta:resourcekey="txtDescriptionResource1"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 19px; text-align: right"></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 49px; text-align: center;">
                        <table id="tblButtons">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" meta:resourcekey="btnRefreshResource1" /></td>
                                <td style="width: 52px">
                                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" meta:resourcekey="btnCloseResource1" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="height: 21px"></td>
        <td style="height: 21px;">&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" meta:resourcekey="ValidationSummary1Resource1" />
        </td>
    </tr>
</table>
