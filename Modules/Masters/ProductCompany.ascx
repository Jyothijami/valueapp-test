<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductCompany.ascx.cs" Inherits="Modules_Masters_ProductCompany" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<link href="../../App_Themes/Master/Master.css" rel="stylesheet" type="text/css" />--%> 
 <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="searchhead" colspan="2" style="text-align: left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            Product Brand Master</td>
                        <td>
                        </td>
                        <td style="text-align: right;">
                            <table border="0" cellpadding="0" cellspacing="0" align="right">
                                <tr>
                                    <td rowspan="3">
                                        <asp:Label id="Label14" runat="server" CssClass="label" EnableTheming="False" Font-Bold="True"
                                            Text="Search By"></asp:Label></td>
                                    <td rowspan="3">
                                        <asp:DropDownList id="ddlSearchBy" runat="server" CssClass="textbox" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--</asp:ListItem>
                                            <asp:ListItem Value="PRODUCT_COMPANY_ID">Sl.No</asp:ListItem>
                                            <asp:ListItem Value="PRODUCT_COMPANY_NAME">Brand Name</asp:ListItem>
                                            <asp:ListItem Value="PRODUCT_COMPANY_DESC">Description</asp:ListItem>
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
                <asp:GridView id="gvCompanyDetails" SelectedRowStyle-BackColor="#c0c0c0" runat="server" AutoGenerateColumns="False"
                    DataSourceID="sdsCompanyDetails" AllowPaging="True" OnRowDataBound="gvCompanyDetails_RowDataBound" Width="100%">
                    <columns>
<asp:BoundField DataField="PRODUCT_COMPANY_NAME" HeaderText="BrandNameHidden"></asp:BoundField>
<asp:BoundField DataField="PRODUCT_COMPANY_ID" HeaderText="Sl.No">
<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Brand Name"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("Dept_name") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
<ItemTemplate>
<asp:LinkButton id="lbtnCompanyName" ForeColor="#0066ff" onclick="lbtnCompanyName_Click" runat="server" Text="<%# Bind('PRODUCT_COMPANY_NAME') %>" CausesValidation="False"></asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="PRODUCT_COMPANY_DESC" HeaderText="Description" NullDisplayText="-">
<ItemStyle HorizontalAlign="Left"></ItemStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
</asp:BoundField>
</columns>
                    <selectedrowstyle backcolor="LightSteelBlue" />
                    <EmptyDataTemplate>
                        No Record Found
                    </EmptyDataTemplate>
                </asp:GridView><asp:SqlDataSource id="sdsCompanyDetails" runat="server" ConnectionString="<%$ ConnectionStrings:DBCon %>" SelectCommand="SP_MASTER_PRODUCT_COMPANY_SEARCH_SELECT" SelectCommandType="StoredProcedure"><selectparameters>
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
            <td colspan="2" style="text-align: center">
                <table id="Table1" align="center">
                    <tr>
                        <td>
                            <asp:Button id="btnNew" runat="server" Text="New" OnClick="btnNew_Click" CausesValidation="False" /></td>
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
                <table style="width: 783px" border="0" cellpadding="0" cellspacing="0" id="tblCompanyDetails" runat="server" visible="false" align="center">
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
            <td style="text-align: right"><asp:Label id="lblDepartmentName" runat="server" Text="Brand Name :" Width="153px"></asp:Label></td>
            <td style="text-align: left;"><asp:TextBox id="txtCompanyName" runat="server" MaxLength="20" OnTextChanged="txtDepartmentName_TextChanged"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" EnableTheming="False" Font-Bold="False" Font-Names="Verdana"
                    Font-Size="Smaller" ForeColor="Red" Text="*"></asp:Label>
                <asp:RequiredFieldValidator id="RFVDeptName" runat="server" ControlToValidate="txtCompanyName"
                    ErrorMessage="Please Enter the Company Name">*</asp:RequiredFieldValidator><cc1:filteredtextboxextender id="ftxteDepartmentName" runat="server" filtermode="InvalidChars"
                    invalidchars="0123456789~!@#$%^=&*()_+|}{&quot;:';/.,?><" targetcontrolid="txtCompanyName"></cc1:filteredtextboxextender>
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