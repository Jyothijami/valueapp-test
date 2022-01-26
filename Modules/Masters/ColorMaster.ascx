<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ColorMaster.ascx.cs" Inherits="Modules_Masters_ItemCategoryMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" /> --%>
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
        <td class="searchhead" colspan="2" style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="text-align: left;">Colour Master</td>
                    <td></td>
                    <td style="text-align: right;">
                        <table border="0" cellpadding="0" cellspacing="0" align="right">
                            <tr>
                                <td rowspan="3">
                                    <asp:Label ID="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                        Text="Search By"></asp:Label></td>
                                <td rowspan="3">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--</asp:ListItem>
                                        <asp:ListItem Value="COLOUR_NAME">Colour</asp:ListItem>
                                        <asp:ListItem Value="ITEM_CATEGORY_NAME">Category Name</asp:ListItem>
                                        <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td rowspan="3">
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="textbox"></asp:TextBox>&nbsp;
                                </td>
                                <td rowspan="3">
                                    <asp:DropDownList ID="ddlCat" runat ="server" CssClass="textbox" AutoPostBack="True" Width ="90px" Visible ="false" EnableTheming ="false"   ></asp:DropDownList>
                                </td>
                                
                                <td rowspan="3">
                                    <asp:Button ID="btnSearchGo" runat="server" BorderStyle="None" CausesValidation="False"
                                        CssClass="gobutton" EnableTheming="False" Text="Go" OnClick="btnSearchGo_Click" /></td>
                            </tr>
                            <tr>
                            </tr>
                            <tr>
                            </tr>
                        </table>
                        <asp:Label ID="lblSearchItemHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchValueHidden" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblSearchcatHidden" runat ="server" Visible ="false"  ></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:GridView ID="gvCategoryDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="gvCompanyDetails_RowDataBound" DataSourceID="sdsCategoryDetails" Width="100%">
                <Columns>
                    <asp:BoundField DataField="COLOUR_NAME" HeaderText="ColourNameHidden"></asp:BoundField>
                    <asp:BoundField DataField="COLOUR_ID" HeaderText="S.No">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Colour Name">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ITEM_CATEGORY_NAME") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCompanyName" OnClick="lbtnCompanyName_Click" ForeColor="#0066ff" runat="server" Text="<%# Bind('COLOUR_NAME') %>" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="Brand" NullDisplayText="-">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ITEM_CATEGORY_NAME" HeaderText="Item Category" />
                    <asp:BoundField DataField="BRAND_ID" HeaderText="BRAND_ID" SortExpression="BRAND_ID" />
                    <asp:BoundField DataField="IC_ID" HeaderText="IC_ID" SortExpression="IC_ID" />
                </Columns>
                <SelectedRowStyle BackColor="LightSteelBlue" />
                <EmptyDataTemplate>
                    No Record Found
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsCategoryDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_COLOR_SEARCH_SELECT" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="lblSearchItemHidden" DefaultValue="0" Name="SearchItemName"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="lblSearchValueHidden" DefaultValue="0" Name="SearchValue"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID ="lblSearchcatHidden" DefaultValue ="0" Name="SearchCat"
                        PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: left"></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center;">
            <table id="Table1" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" Font-Underline="False" /></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False" /></td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CausesValidation="False" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="tblCompanyDetails" runat="server" visible="false">
                <tr>
                    <td colspan="2" style="text-align: left" class="profilehead">General Details</td>
                </tr>
                <tr>
                    <td style="text-align: right"></td>
                    <td style="text-align: left"></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label25" runat="server" Text="Brand :" Width="45px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlBrandStock" runat="server" AutoPostBack="True"
                            Width="149px">
                        </asp:DropDownList><asp:Label ID="Label26" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*"> </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                ControlToValidate="ddlBrandStock" ErrorMessage="Please Select the Brand" InitialValue="0">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label28" runat="server" Text="Category:" Width="84px"></asp:Label></td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" Width="149px">
                        </asp:DropDownList><asp:Label ID="Label29" runat="server" EnableTheming="False" ForeColor="Red"
                            Text="*">
                        </asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                            ControlToValidate="ddlCategory" ErrorMessage="Please Select the Category" InitialValue="0">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lblDepartmentName" runat="server" Text="Colour Name :" Width="153px"></asp:Label></td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="0"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                            Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVDeptName" runat="server" ControlToValidate="txtCategoryName"
                            ErrorMessage="Please Enter the Category Name">*</asp:RequiredFieldValidator><cc1:FilteredTextBoxExtender ID="ftxteDepartmentName" runat="server" FilterMode="InvalidChars"
                                InvalidChars="0123456789~!@#$%^=&*()_+|}{&quot;:';/.,?><" TargetControlID="txtCategoryName">
                            </cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;"></td>
                    <td style="text-align: left;"></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <table id="tblButtons" align="Center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" /></td>
                                <td>
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" CausesValidation="False" /></td>
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
            <td style="height: 21px"></td>
            <td style="height: 21px;"></td>
        </tr>
</table>