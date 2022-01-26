<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ColorMaster.ascx.cs" Inherits="Modules_Masters_ItemCategoryMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" /> --%>
<table style="width: 783px" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            Item Category Master</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="ITEM_CATEGORY_ID">S.No</asp:ListItem>
                                            <asp:ListItem Value="ITEM_CATEGORY_NAME">Category Name</asp:ListItem>
                                            <asp:ListItem Value="ITEM_CATEGORY_DESC">Description</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td rowspan="3">
                                        </td>
                                    <td rowspan="3">
                                        <asp:TextBox id="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button id="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                            CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                </tr>
                            </table>
                            <asp:Label id="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label id="lblSearchValueHidden" runat="server" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:GridView id="gvCategoryDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="gvCompanyDetails_RowDataBound" DataSourceID="sdsCategoryDetails">
                    <columns>
<asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="CategoryNameHidden"></asp:BoundField>
<asp:BoundField DataField="ITEM_CATEGORY_ID" HeaderText="S.No">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Category Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("ITEM_CATEGORY_NAME") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnCompanyName" onclick="lbtnCompanyName_Click" runat="server" Text="<%# Bind('ITEM_CATEGORY_NAME') %>" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="ITEM_CATEGORY_DESC" HeaderText="Description" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                </asp:GridView><asp:SqlDataSource id="sdsCategoryDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_ITEM_CATEGORY_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchItemName" ControlID="lblSearchItemHidden"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" DefaultValue="0" Name="SearchValue" ControlID="lblSearchValueHidden"></asp:ControlParameter>
</selectparameters></asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 19px; text-align: left">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; height: 49px;">
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" Font-Underline="False" /></td>
                        <td>
                            <asp:Button id="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblCompanyDetails" runat="server" visible="false">
                    <tr>
            <td colspan="2" style="text-align: left" class="profilehead">
                General Details</td>
                    </tr>
                    <tr>
            <td style="height: 21px; text-align: right">
            </td>
            <td style="height: 21px; text-align: left">
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right"><asp:Label id="lblDepartmentName" runat="server" Text="Category Name :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtCategoryName" runat="server" MaxLength="20" ></asp:TextBox>
                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RFVDeptName" runat="server" ControlToValidate="txtCategoryName"
                    ErrorMessage="Please Enter the Category Name">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteDepartmentName" runat="server" filtermode="InvalidChars"
                    invalidchars="0123456789~!@#$%^=&*()_+|}{&quot;:';/.,?><" targetcontrolid="txtCategoryName"></cc1:filteredtextboxextender>
            </td>
                    </tr>
                    <tr>
            <td style="text-align: right">
                <asp:Label id="lblDescription" runat="server" Text="Description :" Width="105px"></asp:Label></td>
            <td style="text-align: left"><asp:TextBox id="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
            <td style="text-align: right; height: 19px;">
            </td>
            <td style="text-align: left; height: 19px;">
            </td>
                    </tr>
                    <tr>
            <td colspan="2" style="text-align: center">
                <table id="tblButtons">
                    <tr>
                        <td>
                            <asp:Button id="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                        <td>
                            <asp:Button id="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
                        <td>
                            <asp:Button id="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
                    </tr>
                </table>
                <asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False">
                </asp:ValidationSummary>
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td style="height: 21px;">
            </td>
        </tr>
    </table>